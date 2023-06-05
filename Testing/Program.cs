using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
    static void Main(string[] args)
    {
        // Создаем; экземпляр веб-драйвера
        IWebDriver driver = new ChromeDriver();

        // Переходим на главную страницу
        driver.Navigate().GoToUrl("http://svyatoslav.biz/testlab/wt/");

        // Проверяем наличие слов "menu" и "banners" на главной странице
        bool isMenuPresent = driver.PageSource.Contains("menu");
        bool isBannersPresent = driver.PageSource.Contains("banners");
        Console.WriteLine();
        Console.WriteLine("Проверяем a. Наличие слов «menu» и «banners» на главной странице:");
        Console.WriteLine("Результат: " + (isMenuPresent && isBannersPresent));
        Console.WriteLine();

        // Проверяем наличие текста "CoolSoft by Somebody" в нижней ячейке таблицы
        string pageSource = driver.PageSource;
        string searchText = "© CoolSoft by Somebody";

        bool isTextPresent = pageSource.Contains(searchText);
        Console.WriteLine("Проверяем b. Наличие текста «CoolSoft by Somebody» в нижней ячейке таблицы:");
        Console.WriteLine("Результат: " + isTextPresent);
        Console.WriteLine();

        // Получаем элементы текстовых полей "Рост", "Вес" и "Имя" и поля "Пол"
        IWebElement heightInput = driver.FindElement(By.Name("height"));
        IWebElement weightInput = driver.FindElement(By.Name("weight"));
        IWebElement nameInput = driver.FindElement(By.Name("name"));
        //IWebElement maleRadioButton = driver.FindElement(By.XPath("//input[@name='gender' and @value='male']"));
        //IWebElement femaleRadioButton = driver.FindElement(By.XPath("//input[@name='gender' and @value='female']"));
        IWebElement genderInput = driver.FindElement(By.Name("gender"));

        // Проверяем, являются ли текстовые поля пустыми и не выбрано ли значение в поле "Пол" по умолчанию
        bool areTextFieldsEmpty = string.IsNullOrEmpty(heightInput.GetAttribute("value"))
        && string.IsNullOrEmpty(weightInput.GetAttribute("value"));
        bool isGenderNotSelected = !genderInput.Selected;
        Console.WriteLine("Проверка c. Текстовые поля формы по умолчанию пусты, а пол не выбран:");
        Console.WriteLine("Результат: " + (areTextFieldsEmpty && isGenderNotSelected));
        Console.WriteLine();

        // Заполняем поля "Рост" и "Вес" значениями и отправляем форму
        heightInput.SendKeys("50");
        weightInput.SendKeys("3");
        nameInput.SendKeys("Nikita Dubrovin");
        genderInput.Click();
        //femaleRadioButton.Click();
        IWebElement submitButton = driver.FindElement(By.XPath("//input[@type='submit']"));
        submitButton.Click();

        // Проверяем появление сообщения "Слишком большая масса тела"
        bool isErrorMessageDisplayed = driver.PageSource.Contains("Слишком большая масса тела");
        Console.WriteLine("Проверка d. После заполнения поля «Рост» значением «50» и поля «Вес» значением «3» и отправки формы, " +
        "форма исчезает и появляется сообщение «Слишком большая масса тела»:");
        Console.WriteLine("Результат: " + isErrorMessageDisplayed);
        Console.WriteLine();
        driver.Navigate().Back();

        // Проверяем наличие формы с текстовыми полями, радио-кнопками и кнопкой на главной странице
        bool isFormPresent = driver.FindElement(By.TagName("form")).Displayed;
        Console.WriteLine("Проверка e. На главной странице приложения после открытия присутствует форма " +
        "с тремя текстовыми полями, группой из двух радио-кнопок и одной кнопкой:");
        Console.WriteLine("Результат: " + isFormPresent);
        Console.WriteLine();
        // Проверяем появление сообщений об ошибках при неверном вводе значений веса и роста
        heightInput.Clear();
        weightInput.Clear();
        submitButton.Click();
        bool isHeightErrorMessageDisplayed = driver.PageSource.Contains("Рост должен быть в диапазоне 50-300 см.");
        bool isWeightErrorMessageDisplayed = driver.PageSource.Contains("Вес должен быть в диапазоне 3-500 кг.");
        Console.WriteLine("Проверка f. При неверном вводе значений веса и/или роста появляются сообщения " +
        "о том, что рост должен быть в диапазоне от 50 до 300 см, а вес - от 3 до 500 кг:");
        Console.WriteLine("Результат: " + (isHeightErrorMessageDisplayed && isWeightErrorMessageDisplayed));
        Console.WriteLine();
        // Проверяем наличие текущей даты на главной странице в формате "DD.MM.YYYY"
        string currentDate = DateTime.Now.ToString("dd.MM.yyyy");
        bool isDatePresent = driver.PageSource.Contains(currentDate);
        Console.WriteLine("Проверка g. Главная страница содержит текущую дату в формате «DD.MM.YYYY»:");
        Console.WriteLine("Результат: " + isDatePresent);

        // Закрываем веб-драйвер
        driver.Quit();
    }
}
