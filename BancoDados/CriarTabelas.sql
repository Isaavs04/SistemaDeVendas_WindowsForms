
CREATE TABLE Categoria (
    CdCategoria INT PRIMARY KEY,
    NomeCategoria VARCHAR(100) NOT NULL
);

CREATE TABLE Produto (
    CdProduto INT PRIMARY KEY,
    NomeProduto VARCHAR(100) NOT NULL,
    PrecoProduto DECIMAL(10,2) NOT NULL,
    CdCategoria INT NOT NULL,

    CONSTRAINT FK_Produto_Categoria
    FOREIGN KEY (CdCategoria)
    REFERENCES Categoria(CdCategoria)
);