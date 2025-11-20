using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Models
{
    public class Document
    {
        public readonly static string INSERTDOCUMENT = "INSERT INTO tblDocumentos " +
                                        "(ClienteID, TipoDocumento, Numero, DataEmissao, DataValidade) " +
                                        "VALUES (@CustomerID, @DocumentType, @Number, @EmissionDate, @ExpirationDate)";

        public readonly static string UPDATEDOCUMENT = @"UPDATE tblDocumentos 
                                                        SET TipoDocumento = @DocumentType, 
                                                        Numero = @Number, 
                                                        DataEmissao = @EmissionDate, 
                                                        DataValidade = @ExpirationDate
                                                        WHERE ClienteID = @CustomerID;";

        public int DocumentID { get; private set; }
        public int CustomerID { get; private set; }
        public string DocumentType { get; private set; }
        public string Number { get; private set; }
        public DateOnly EmissionDate { get; private set; }
        public DateOnly ExpirationDate { get; private set; }

        public Document(string documentType, string number, DateOnly emissionDate, DateOnly expirationDate)
        {
            DocumentType = documentType;
            Number = number;
            EmissionDate = emissionDate;
            ExpirationDate = expirationDate;
        }

        public void SetCustomerID(int customerID)
        {
            CustomerID = customerID; 
        }

        public override string? ToString()
        {
            return $"Type: {DocumentType}\nNumber: {Number}\n" +
                $"Emission Date: {EmissionDate}\nExpiration Date: {ExpirationDate}\n";
        }
    }
}
