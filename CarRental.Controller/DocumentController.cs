using CarRental.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using Utils.Databases;

namespace CarRental.Controller
{
    public class DocumentController
    {
        private readonly SqlConnection Connection = new(ConnectionDB.GetConnectionString());
        public void AddDocument(Document document, SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                var command = new SqlCommand(Document.INSERTDOCUMENT, connection, transaction);

                command.Parameters.AddWithValue("@CustomerID", document.CustomerID);
                command.Parameters.AddWithValue("@DocumentType", document.DocumentType);
                command.Parameters.AddWithValue("@Number", document.Number);
                command.Parameters.AddWithValue("@EmissionDate", document.EmissionDate);
                command.Parameters.AddWithValue("@ExpirationDate", document.ExpirationDate);

                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error inserting new document: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error inserting new document: " + ex.Message);
            }
        }
    }
}
