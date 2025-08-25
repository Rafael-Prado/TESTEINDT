using Dapper;
using Microsoft.Data.SqlClient;

namespace InfraProposta.Data
{
  
    public class MigrationService
    {
        private readonly string _connectionString;

        public MigrationService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Run()
        {
            using var connectionMaster = new SqlConnection("Server=host.docker.internal,1433;Database=Master;User Id=sa;Password=YourStrong!Passw0rd;Encrypt=True;TrustServerCertificate=True;");
            connectionMaster.Open();

            connectionMaster.Execute(@"
                    IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'Seguro')
                    BEGIN
                        CREATE DATABASE [Seguro];
                    END
    
            ");

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            connection.Execute(@"
                IF OBJECT_ID('Proposta', 'U') IS NULL
                BEGIN
                    CREATE TABLE Proposta (
                    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
                    IdCliente UNIQUEIDENTIFIER NOT NULL,
                    IdProduto UNIQUEIDENTIFIER NOT NULL,
                    Valor DECIMAL(18,2) NOT NULL,
                    Status INT NOT NULL
                );
                END

                IF OBJECT_ID('Contratacoes', 'U') IS NULL
                BEGIN
                    CREATE TABLE Contratacoes (
                    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
                    PropostaId UNIQUEIDENTIFIER NOT NULL,
                    DataContratacao DATE NOT NULL
                );
                END
                IF OBJECT_ID('Usuario', 'U') IS NULL
                    BEGIN
                        CREATE TABLE Usuario (
                            Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
                            Cpf NVARCHAR(20) NOT NULL,
                            Password NVARCHAR(100) NOT NULL,
                            Name NVARCHAR(100) NOT NULL
                        );
                    END

                    -- Inserção de dados de teste
                    IF NOT EXISTS (SELECT 1 FROM Usuario)
                    BEGIN
                        INSERT INTO Usuario (Id, Cpf, Password, Name) VALUES
                            (NEWID(), '123.456.789-00', 'senha123', 'João Silva'),
                            (NEWID(), '987.654.321-00', 'minhasenha', 'Maria Souza');
                    END
            ");
        }
    }
}
