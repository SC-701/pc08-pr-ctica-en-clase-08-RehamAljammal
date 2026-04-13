CREATE PROCEDURE dbo.AgregarProductos
     @Id             UNIQUEIDENTIFIER,
     @IdSubCategoria UNIQUEIDENTIFIER,
     @Nombre         VARCHAR(MAX),
     @Descripcion    VARCHAR(MAX),
     @Precio         DECIMAL(18,2),
     @Stock          INT,
     @CodigoBarras   VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    INSERT INTO dbo.Producto
        (Id, IdSubCategoria, Nombre, Descripcion, Precio, Stock, CodigoBarras)
    VALUES
        (@Id, @IdSubCategoria, @Nombre, @Descripcion, @Precio, @Stock, @CodigoBarras);

    COMMIT TRANSACTION;

    SELECT @Id;
END
