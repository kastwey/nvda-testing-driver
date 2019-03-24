# Nvda Testing Driver: Automatiza tus tests con un lector de pantalla

## Introducción. ¿Qué es NvdaTestingDriver?

NvdaTestingDriver es un paquete que nos permitirá realizar tests funcionales de accesibilidad, utilizando para ello el lector de pantalla NVDA.
El paquete es capaz de iniciar el lector de pantalla (no necesita instalación, pues ya incluye una versión portable ), y manejarlo como si fuese el propio usuario el que interactúa con él, enviándole órdenes y recibiendo las respuestas textuales. Con dichas respuestas, podremos confeccionar nuestros tests, que fallarán si los textos recibidos no se corresponden con lo que consideramos que el lector debería pronunciar en cada momento.

NvdaTestingDriver, aunque es un paquete NetStandard, solo es compatible con Windows (versiones de Windows 7 SP 1 en adelante), ya que NVDA es un lector de pantallas desarrollado para este sistema operativo.

En este repositorio podréis descargar el código fuente, así como un proyecto webt y un proyecto de test, que utilizará la web de ejemplo para ejecutar los tests funcionales.

## ¿Qué es un lector de pantalla?

Un lector de pantalla es un software que es capaz de identificar e interpretar lo que aparece en la pantalla de un ordenador, y trasladarlo al usuario mediante una síntesis de voz o una pantalla braille. Así, este software permite a una persona ciega o con baja visión manejar un ordenador, al poder leer lo que ocurre en pantalla en todo momento.

Pero para que el lector de pantalla sea capaz de leer una aplicación o sitio web, este tiene que ser accesible.

Partiendo de esta premisa, NVDA es una herramienta maravillosa para testear si nuestro sitio web es accesible o no, ya que el lector debe ser capaz de convertir todo nuestro sitio web a texto que una persona ciega o con baja visión pueda leer: imágenes, estructura del sitio, formularios...

## ¿Cómo funciona Nvda Testing Driver?

Nvda Testing Driver nos permitirá ejecutar el lector de pantallas NVDA de manera automática, manejarlo desde .Net, obtener las respuestas textuales que el lector de pantalla anunciaría en voz alta, y comparar dichas respuestas con los textos que esperamos que el lector pronuncie, si el componente a testear funciona como se espera.

Así, por ejemplo, si tenemos una página de contacto, y sabemos que en el cuadro de edición del nombre, NVDA debería leer: "Name: text edit", podríamos crear un test que navegue hasta la página de contacto, se posicione sobre el cuadro de edición, obtenga el texto que NVDA devolverá al posicionarse sobre él, y compararlo con lo que sabemos que debería pronunciar. Si los textos coinciden, el componente se lee como se espera, y el test pasa. De lo contrario, algo ha fallado, los textos no coinciden, y el test fallará.


Este ejemplo es muy sencillo, pero imaginad un componente más complejo, como una vista de árbol que hemos programado con Aria. Si manualmente comprobamos que dicha vista funciona bien con NVDA, podremos programar un test que se posicione sobre ella, navegue expandiendo y colapsando nodos, y compare los textos devueltos por NVDA con lo que sabemos que debería pronunciar.
Así, si en algún momento durante el desarrollo de la web la vista de árbol deja de funcionar correctamente, la ejecución automática del test nos permitirá darnos cuenta de manera rápida, detectar el fallo y corregirlo, sin tener que estar comprobando de manera manual todas las páginas para asegurarnos de que siguen manteniendo un buen nivel de accesibilidad con un lector de pantalla.
En los tests de ejemplo tenéis un test que interactúa con una vista de árbol y que garantiza que NVDA funciona correctamente con ella.

## Cómo obtengo los textos que NVDA debería pronunciar para construir mis tests

NVDA posee una herramienta genial llamada "Voice viewer". Dicha herramienta guardará en un registro todo lo que NVDA pronuncie durante una sesión. Así, para construir nuestro test, no tendremos más que iniciar el lector de pantallas, habrir la herramienta "Voice Viewer", navegar al sitio web, interactuar con los elementos que queramos testear, y a continuación, copiar el registro de todo lo pronunciado por NVDA y construir nuestros tests de acuerdo a esos textos.

## Comandos de NVDA

NVDA posee una gran cantidad de órdenes para ejecutar multitud de acciones. Para facilitar la interacción y prevenir errores de codificación, esta librería incluye, como comandos ya definidos,  las funciones más relevantes de NVDA, escritas en varias clases estáticas dentro del espacio de nombres `NvdaTestingDriver.Commands.NvdaCommands`. Cada clase contiene una categoría, tal y como podemos ver en la [ayuda de NVDA](https://www.nvaccess.org/files/nvda/documentation/userGuide.html?).


## Flujo de ejecución

### Instanciar un  objeto de tipo `NvdaTestingDriver`

Lo primero que tenemos que hacer es instanciar la clase `NvdaTestingDriver`. Esta instanciación podemos hacerla sin parámetros (NVDA se cargará con las opciones por defecto), o bien pasándole una función que recibirá como parámetro el objeto `NvdaOptions` , el cuál podremos modificar para modificar el comportamiento por defecto del lector de pantalla. Por ejemplo:

```csharp
			var nvdaDriver = new NvdaDriver(opt =>
			{
				opt.GeneralSettings.Language = NvdaTestingDriver.Settings.NvdaLanguage.English;
			});

```

### Iniciar NVDA

El siguiente paso es conectar el driver, es decir, iniciar NVDA y conectarnos a él para controlarlo.

```csharp
await nvdaTestingDriver.ConnectAsync();
```

### Interactuando con NVDA

El tercer paso es interactuar con NVDA. 

Esta librería contiene varios métodos para enviar comandos a NVDA y recibir las respuestas textuales del lector de pantalla. Con la respuesta de NVDA, podremos hacer las comparaciones con los textos que esperamos recibir, y determinar así si algún componente no se está comportando como debería.

**Importante**: Recordad que para hacer comparaciones deberéis usar el método `TextContains` de la clase NvdaTestHelper, o bien, si queréis que el método lance una excepción de tipo AssertFailedException, utilizad el método `TextContains` de la clase `NvdaAssert` (paquete NvdaTestingDriver.MSTest).

Aquí os explicamos los métodos más importantes:

#### `SendCommandAndGetSpokenTextAsync`

Este método recibirá un comando (puede ser predefinido o construiído de forma manual indicándole las combinaciones de teclas a enviar), y devolverá la respuesta que NVDA verbalizará a resultas de ese comando.

Por ejemplo:

```csharp
string text = await nvdaDriver.SendCommandAndGetSpokenTextAsync(BrowseModeCommands.NextFormField);
```

Esta instrucción enviará el comando `BrowseModeCommands.NextFormField` a NVDA (equivalente a enviar la pulsación de teclas `F`, esperará a que NVDA envíe una respuesta, y devolverá esa respuesta, que se almacenará en la variable `text`. Podremos utilizar esa variable para hacer las comparaciones necesarias y asegurarnos de que el comando responde como se espera.


#### `SendKeysAndGetSpokenTextAsync`

Este método recibirá una lista de teclas (serán tratadas como una combinación), y devolverá la respuesta que NVDA verbalizará a resultas de esas pulsaciones.

Por ejemplo:

```csharp
string text = await nvdaDriver.SendKeysAndGetSpokenTextAsync(Key.DownArrow);
```

Esta instrucción enviará la tecla `DownArrow` (flecha abajo) a NVDA, esperará a que NVDA envíe una respuesta, y devolverá esa respuesta, que se almacenará en la variable `text`. Podremos utilizar esa variable para hacer las comparaciones necesarias y asegurarnos de que el comando responde como se espera.

### Desconectarnos y salir de NVDA

El último paso es cerrar la conexión con NVDA y descargar el lector de pantalla:

```csharp
			await NvdaDriver.DisconnectAsync();
			```

Este comando desconectará el control remoto hacia NVDA, y a continuación, descargará el lector de pantalla.

Ya que `NvdaTestingDriver` implementa la interfaz `IDisposable`, si incluímos la instanciación de la clase dentro de un bloque `using`, al cerrar el bloque, se ejecutará el método `Dispose`, y éste llamará al método `DisconnectAsync`, cerrando el control remoto y descargando NVDA.

## Creando nuestros tests

Imaginad que queremos verificar que los botones de Github son accesibles.

1. Cread un proyecto nuevo de tipo *MSTest Test Project (.NET Core)*
2. Asignadle un nombre y una ubicación.
3. Instalad los siguientes paquetes:
    - NvdaTestingDriver
    - NvdaTestingDriver.MSTest
    - Selenium.WebDriver 
    - Selenium.Chrome.WebDriver 
    - NvdaTestingDriver.Selenium
4. Descargad el [https://www.nvaccess.org/download/](lector de pantalla NVDA)
5. Instalad NVDA, o cread una copia portable (el instalador os dará esa opción).
6. Entrad a Github utilizando Chrome. Yo he utilizado como ejemplo la página de DotNetCore: [https://github.com/dotnet/core](https://github.com/dotnet/core).
7. Iniciad NVDA. La primera vez, os aparecerá el diálogo de bienvenida. Elegid la disposición de teclado que más os convenga y pulsad en Aceptar.
8. Abrid el menú de NVDA pulsando Insert + N. Id a *Herramientas* y haced click en *Visualizador de voz*.
9. Se abrirá una ventana en la que aparecerá todo lo que NVDA irá pronunciando.
10. Volved a la ventana de Github. Una vez en la ventana, pulsad la tecla *D* para moveros entre regiones de la página. Pulsad la tecla cuatro veces hasta que NVDA esté posicionado en la región de *Repository Navigation*, y entonces, pulsad la tecla *B* dos veces hasta que NVDA esté posicionado sobre el botón para elegir la rama. NVDA dirá algo como: `button  collapsed  subMenu Branch: master`. En este momento, NVDA está sobre el botón, pero el foco del sistema no, ya que NVDA utiliza un buffer virtual para navegar entre controles. Para forzar que el foco se posicione sobre el elemento actual del buffer virtual, pulsad dos veces  *insert+shift+numpadMinus* (si elejísteis la disposición de teclado de escritorio), o *shift+insert+backspace* (si elegísteis la disposición de portátil).
11. Pulsad el comando para que NVDA lea el elemento que tiene el foco (insert+tab). NVDA leerá algo como: `Branch: master   button  focused collapsed  subMenu  Switch branches or tags`.
12. Volved a la ventana del *visualizador de voz*. Veréis que en una de las últimas líneas, aparecerá el texto que NVDA pronunció cuando pulsamos el comando insert+tab: `Branch: master   button  focused  collapsed  subMenu  Switch branches or tags`. Copiad ese texto.
13. Cerrad NVDA pulsando `Insert + Q`.
14. Id a la ventana de *Visual Studio*, y en el proyecto, cread una nueva clase llamada: `TestHelper`.
15. En la clase, pegad el siguiente contenido: 

        using System;
        using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
        using NvdaTestingDriver;
        using NvdaTestingDriver.Selenium;
        using OpenQA.Selenium;
        using OpenQA.Selenium.Chrome;
        
        namespace RemoteWebsites.Tests
        {
        	[TestClass]
        	public static class TestHelper
        	{
        
        		internal static WebDriverWrapper WebDriverWrapper = new WebDriverWrapper();
        
        		internal static IWebDriver WebDriver { get; private set; }
        
        
        		internal static NvdaDriver NvdaDriver;
        
        		/// <summary>
        		/// This method will be executed before starting any test
        		/// </summary>
        		/// <param name="context">The context.</param>
        		/// <returns>The task associated to this operation</returns>
        		[AssemblyInitialize]
        		public static async Task Initialize(TestContext context)
        		{
        
        			// Initialize the Selenium WebDriveer
        			UpWebDriver();
        
        			// Starts the NVDATestingDriver
        			await ConnectNvdaDriverAsync();
        		}
        
        		private static void UpWebDriver()
        		{
        			try
        			{
        
        				// We started the WebDriver using the UpWebDriver method of the WebDriverWrapper class,
        				// to manage the chrome window, and get to put it in the foreground when necessary.
        				WebDriver = WebDriverWrapper.UpWebDriver(() =>
        				{
        					var op = new ChromeOptions
        					{
        						AcceptInsecureCertificates = false
        					};
        					var webDriver = new ChromeDriver(Environment.CurrentDirectory, op);
        					webDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromMinutes(3);
        					webDriver.Manage().Window.Maximize();
        					return webDriver;
        				});
        			}
        			catch (Exception ex)
        			{
        				Console.WriteLine($"Error while starting Chrome WebDriver: {ex.Message}");
        				throw;
        			}
        		}
        
        
        		/// <summary>
        		/// Connects the nvda driver asynchronously.
        		/// </summary>
        		/// <returns></returns>
        		private static async Task ConnectNvdaDriverAsync()
        		{
        			try
        			{
        				// We start the NvdaTestingDriver:
        				NvdaDriver = new NvdaDriver(opt =>
        				{
        					opt.GeneralSettings.Language = NvdaTestingDriver.Settings.GeneralSettings.NvdaLanguage.English;
        				});
        				await NvdaDriver.ConnectAsync();
        			}
        			catch (Exception ex)
        			{
        				Console.WriteLine($"Error while starting NVDA driver: {ex.Message}");
        				throw;
        			}
        		}
        
        		/// <summary>
        		/// This method will be executed when all e tests are finished.
        		/// </summary>
        		/// <returns></returns>
        		[AssemblyCleanup]
        		public static async Task Cleanup()
        		{
        			try
        			{
        				WebDriver.Quit();
        			}
        			catch
        			{
        				// If the web  driver quit fails, we continue.
        			}
        
        			try
        			{
        				WebDriver.Dispose();
        			}
        			catch
        			{
        				// If the web  driver dispose fails, we continue.
        			}
        
        			try
        			{
        				await NvdaDriver.DisconnectAsync();
        			}
        			catch
        			{
        				// if disconnect fails, we continue.
        			}
        		}
        
        
        
        	}
        }

16. Crea una segunda clase, donde almacenaremos nuestro método de test:

        using System.Threading.Tasks;
        using Microsoft.VisualStudio.TestTools.UnitTesting;
        using NvdaTestingDriver.Commands.NvdaCommands;
        using NvdaTestingDriver.MSTest;
        using NvdaTestingDriver.Selenium.Extensions;
        using OpenQA.Selenium;

        namespace RemoteWebsites.Tests
        {
        	[TestClass]
        	public class GithubRepoPageShould
        	{
        
        		[TestMethod]
        		public async Task CheckDownloadButtonIsCollapsibleAndExpandibleAsync()
        		{
        			
        			// Arrange:
			        // We tell the WebDriverWrapper to put the Chrome window in the foreground.
        			// NVDA needs the window to be in the foreground in order to interact with that window.
        			TestHelper.WebDriverWrapper.SetBrowserWindowForeground();
        
        			// Go to dotnet core github repository:
        			TestHelper.WebDriver.Navigate().GoToUrl("https://github.com/dotnet/core");
					// We put the focus on chrome window:
        			TestHelper.WebDriver.FocusOnWindow();
        
        			// We put the focus inside the first summary tag with btn class:
        			TestHelper.WebDriver.Focus(TestHelper.WebDriver.FindElement(By.CssSelector("summary.btn")));
        
        			// Act & asserts
        			// We send the ReportCurrentFocus command to NVDA and get the text:
        			string text = await TestHelper.NvdaDriver.SendCommandAndGetSpokenTextAsync(NvdaTestingDriver.Commands.NvdaCommands.NavigatingSystemFocusCommands.ReportCurrentFocus);
        
        			// We use the NvdaAssert.TextContains method to check that the text pronounced by NVDA
        			// contains the text it should say.
        			// This method sanitize both the expected text and the received text, to remove spaces, line breaks and other characters that could affect the result.
        			// Whenever you want to compare text with NVDA,
        			// use either the TextContains method of the NvdaAsert class (NvdaTestingDriver.MSTest package),
        			// which will throw an AssertFailedException if the text specified is not present in the
        			// text pronounced by NVDA, or the method TextContains of the NvdaTestHelper class
        			// (NvdaTestingDriver package), which will return true or false.
        			NvdaAssert.TextContains(text, "Branch: master button focused collapsed sub Menu Switch branches or tags");
        		}
        
        	}
        }

17. Compilad la aplicación y ejecutad el test recién creado. *Importante*: Si estáis  usando un lector de pantalla, deberás desactivarlo antes de ejecutar los tests, ya que este lector de pantalla interferirá con el lector de pantalla que se inicia automáticamente con el test.
18. Siempre y cuando el botón elegido siga manteniendo el mismo comportamiento, este test debería pasar.


## Otros ejemplos

En este repositorio podrás encontrar, dentro de la solución `NvdaTestingDriver`, una carpeta llamada `Examples`, y dentro de ella, dos ejemplos. E primer ejemplo (`RemoteWebsites.Tests`) es el mostrado más arriba, sobre cómo comprobar que un determinado botón de Github es accesible.
El segundo (`AccessibleDemo.Tests)`, se basa en el proyecto `AccessibleDemo`, también incluído en esta carpeta. En este ejemplo hay varios tests que comprobarán la interacción con distintos componentes dentro de esa web.


## Integrando `NvdaTestingDriver` dentro de un flujo de integración contínua

Es posible integrar los tests que utilizan `NvdaTEstingDriver` dentro de un flujo de integración contínua. Yo lo he probado utilizando los pipelines de Azure DevOps, pero tenéis que tener algunas cosas en cuenta:

- Solo podréis utilizar agentes propios, no los agentes autoalojados en Azure. Esto es debido a que los agentes autoalojados utilizan el modo no interactivo, lo cuál significa que aunque `Selenium` abra una página web, esa web nunca estará visible para NVDA.
- Al instalar nuestro propio agente en *Azure Devops*, debemos asegurarnos de que elegimos el modo interactivo, lo cuál permitirá al agente interactuar con la interfaz de Windows, y así, NVDA podrá leer cualquier ventana abierta por nuestros tests.

En conclusión: es posible crear tests de regresión en nuestra web, crear tests con NVDA, crear políticas requeridas para subir código a una rama, añadir la ejecución de nuestros tests en la build de esa política, y si algún test no pasa, rechazar la  pull request. ¡Maravilloso! ;)


## Colabora con `NvdaTestingDriver`

¡Si te apetece colaborar con este proyecto, toda aportación será bienvenida! ¡Enviadme todos los pull requests que consideres, y hagamos que este proyecto crezca con ayuda de todos vosotros!

## Agradecimientos

Este proyecto no habría sido posible sin todas las personas que me han ayudado con algún aspecto del mismo:

- A [Pablo Núñez](https://www.twitter.com/pablonete), que en una conversación por Twitter me dio la idea de hacer tests con lector de pantalla, y fue la semilla de este proyecto. ¡Ojalá puedas usarlo en tus desarrollos! :)
- A [Christopher Toth](https://www.twitter.com/mongoose_q) y a Tyler Spivey, que desarrollaron el maravilloso complemento para NVDA [NvdaRemote](https://www.nvdaremote.com), y que me ha permitido controlar reomtamente NVDA.
- A [Jose Manuel Delicado](https://www.twitter.com/jmdaweb), que me ayudó a entender un poco mejor el complemento de NvdaRemote y a modificarlo para poder depurarlo.
- Y por último y no menos importante, a mi mujer, [Núria](https://www.twitter.com/amaterasu_n), quien aguanta mis horas sentado delante del ordenador, y se encarga de todo durante esas horas en las que estoy inmerso en este mundo de códigos. ¡Gracias, mi vida!
