namespace MyCodeTests

open Xunit
open FsUnit
open DemoCode.CustomerTypes
open DemoCode.Customer

module ``Convert customer to eligible`` =

    let sourceCustomer = { CustomerId = "John"; IsRegistered = true; IsEligible = true }

    [<Fact>]
    let ``should succeed if not currently eligible`` () =
        let customer = { sourceCustomer with IsEligible = false }
        let upgraded = convertToEligible customer
        upgraded |> should equal sourceCustomer

    [<Fact>]
    let ``should return eligible customer unchanged`` () =
        let upgraded = convertToEligible sourceCustomer
        upgraded |> should equal sourceCustomer