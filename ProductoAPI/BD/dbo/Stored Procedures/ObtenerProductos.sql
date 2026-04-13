CREATE PROCEDURE dbo.ObtenerProductos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.Id,
        p.Nombre,
        p.Descripcion,
        p.Precio,
        p.Stock,
        p.CodigoBarras,

        sc.Id AS IdSubCategoria,
        sc.Nombre AS SubCategoria,

        c.Id AS IdCategoria,
        c.Nombre AS Categoria
    FROM dbo.Producto p
    INNER JOIN dbo.SubCategorias sc
        ON p.IdSubCategoria = sc.Id
    INNER JOIN dbo.Categorias c
        ON sc.IdCategoria = c.Id;
END
