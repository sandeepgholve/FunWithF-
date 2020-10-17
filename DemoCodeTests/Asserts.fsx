#load "customer.fs"

open DemoCode.CustomerTypes
open DemoCode.Customer

let assertUpgrade =
    let customer = { CustomerId = "John"; IsRegistered = true; IsEligible = false }
    let upgraded = convertToEligible customer
    upgraded = { customer with IsEligible = true }