using Biblioteca_de_clases_concesionaria;
using Biblioteca_de_clases_concesionaria.carpeta_Excepcion;
using System.Text.Json;

[TestClass]
public class Jsontest
{

    [TestMethod]
    public void validarCrearJsonExitoso()
    {
        // Arrange: Inicialización 

        string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string relativePath = Path.Combine(projectDirectory, @"..\..\..\Aplicasion_WF_Consecionaria\archivosjs\");
        string path = Path.GetFullPath(relativePath);

        string nombre = "Testing.json";
        Daojson daojson = new Daojson(path, nombre);

        // Act
        string retorno = daojson.Crear();

        // Assert
        Assert.IsTrue((Directory.Exists(path)));
            
    }

    [TestMethod]
    [ExpectedException(typeof(ExcepcionJson))]
    public void tryCatchCrear()
    {

        // Arrange: Inicialización 

        string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string relativePath = Path.Combine(projectDirectory, @"..\..\..\Aplicasion_WF_Consecionaria\archivosjs\");
        string path = Path.GetFullPath(relativePath);

        string nombre = "/*z.,.-//////@@";
        Daojson daojson = new Daojson(path, nombre);

        // Act
        string retorno = daojson.Crear();
        
    }


    [TestMethod]
    public void validarCrearJsonYaExiste()
    {
        // Arrange: Inicialización 
        string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string relativePath = Path.Combine(projectDirectory, @"..\..\..\Aplicasion_WF_Consecionaria\archivosjs\");
        string path = Path.GetFullPath(relativePath);
            
        string nombre = "Testing.json";
        Daojson daojson = new Daojson(path, nombre);
            
        // Act
        string retorno = daojson.Crear();
            
        // Assert
        string expectedMessage = "El archivo ya existe.\n";
        Assert.AreEqual(expectedMessage, retorno);
    }
        
   



}

//Microsoft.VisualStudio.TestTools.UnitTesting.Assert
//Gravedad Código	Descripción	Proyecto	Archivo	Línea	Estado suprimido	Detalles
//Error (activo)	CS0104	'Assert' es una referencia ambigua entre 'NUnit.Framework.Assert' y 'Microsoft.VisualStudio.TestTools.UnitTesting.Assert'	TestProject	C:\Users\juan\Desktop\1parcial\Biblioteca_de_clases_concesionaria\TestProject\UnitTest1.cs	24		


