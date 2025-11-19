using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Models
{
    public class Document
    {
        public int DocumentID { get; private set; }
        public int CustomerID { get; private set; }
        public string DocumentType { get; private set; }
        public string Number { get; private set; }
        public DateOnly EmissionDate { get; private set; }
        public DateOnly ExpirationDate { get; private set; }

        public Document(int customerID, string documentType, string number, DateOnly emissionDate, DateOnly expirationDate)
        {
            CustomerID = customerID;
            DocumentType = documentType;
            Number = number;
            EmissionDate = emissionDate;
            ExpirationDate = expirationDate;
        }

        public override string? ToString()
        {
            return $"Type: {DocumentType}\nNumber: {Number}\n" +
                $"Emission Date: {EmissionDate}\nExpiration Date: {ExpirationDate}\n";
        }
    }
}
