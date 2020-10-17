namespace DemoCode

[<AutoOpen>]
module CustomerTypes =

    type Customer = {
        CustomerId: string
        IsRegistered: bool
        IsEligible: bool
    }

    type CreateCustomerException = 
        | RemoteServerException of exn
        | CustomerAlreadyExistsException

module Db =

    let tryGetCustomer customerId =
        try 
            [
                { CustomerId = "John"; IsRegistered = true; IsEligible = true }
                { CustomerId = "Mary"; IsRegistered = true; IsEligible = true }
                { CustomerId = "Richard"; IsRegistered = true; IsEligible = false }
                { CustomerId = "Sarah"; IsRegistered = false; IsEligible = false }
            ]
            |> List.tryFind (fun c -> c.CustomerId = customerId)
            |> Ok
        with 
        | ex -> Error (RemoteServerException ex)

    let saveCustomer (customer: Customer) = 
        try
            Ok ()
        with
        | ex -> Error (RemoteServerException ex)

module Customer =

    let trySaveCustomer saveCustomer customer =
        match customer with
        | Some c -> c |> saveCustomer
        | None -> Ok ()

    let convertToEligible customer =
        if not customer.IsEligible then { customer with IsEligible = true }
        else customer

    let upgradeCustomer customerId = 
        customerId
        |> Db.tryGetCustomer
        |> Result.map (Option.map convertToEligible)
        |> Result.bind (trySaveCustomer Db.saveCustomer)

    (*
    let tryConvertToEligible convertToEligible customer =
        match customer with
        | Some c -> Some (convertToEligible c)
        | None -> None
    *)