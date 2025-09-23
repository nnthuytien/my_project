package Login;

import io.github.bonigarcia.wdm.WebDriverManager;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.Assert;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;

import java.time.Duration;

public class Login {

    WebDriver driver;
    WebDriverWait wait;

    By usernameField = By.id("user-name");
    By passwordField = By.id("password");
    By loginButton   = By.id("login-button");
    By errorMessage  = By.cssSelector("h3[data-test='error']");

    @BeforeMethod
    public void setup() {
        WebDriverManager.chromedriver().setup();
        driver = new ChromeDriver();
        driver.manage().window().maximize();
        wait = new WebDriverWait(driver, Duration.ofSeconds(10));
        driver.get("https://www.saucedemo.com/");
    }

    // Login thành công với user hợp lệ
    @Test
    public void testValidLogin() {
        driver.findElement(usernameField).sendKeys("standard_user");
        driver.findElement(passwordField).sendKeys("secret_sauce");
        driver.findElement(loginButton).click();

        wait.until(ExpectedConditions.urlContains("inventory.html"));
        Assert.assertTrue(driver.getCurrentUrl().contains("inventory.html"),
                "Login failed: Không chuyển đến trang inventory!");
    }

    // Sai password
    @Test
    public void testInvalidPassword() {
        driver.findElement(usernameField).sendKeys("standard_user");
        driver.findElement(passwordField).sendKeys("wrong_pass");
        driver.findElement(loginButton).click();

        WebElement error = wait.until(ExpectedConditions.visibilityOfElementLocated(errorMessage));
        Assert.assertTrue(error.getText().contains("Username and password do not match"),
                "Không hiện thông báo lỗi khi nhập sai password!");
    }

    // Sai username
    @Test
    public void testInvalidUsername() {
        driver.findElement(usernameField).sendKeys("wrong_user");
        driver.findElement(passwordField).sendKeys("secret_sauce");
        driver.findElement(loginButton).click();

        WebElement error = wait.until(ExpectedConditions.visibilityOfElementLocated(errorMessage));
        Assert.assertTrue(error.getText().contains("Username and password do not match"),
                "Không hiện thông báo lỗi khi nhập sai username!");
    }

    // Để trống cả Username và Password
    @Test
    public void testEmptyFields() {
        driver.findElement(loginButton).click();
        WebElement error = wait.until(ExpectedConditions.visibilityOfElementLocated(errorMessage));
        Assert.assertTrue(error.getText().contains("Username is required"),
                "Không hiện thông báo lỗi khi để trống cả 2 trường!");
    }

    // Chỉ nhập Username, để trống Password
    @Test
    public void testEmptyPassword() {
        driver.findElement(usernameField).sendKeys("standard_user");
        driver.findElement(loginButton).click();

        WebElement error = wait.until(ExpectedConditions.visibilityOfElementLocated(errorMessage));
        Assert.assertTrue(error.getText().contains("Password is required"),
                "Không hiện thông báo lỗi khi để trống password!");
    }

    // User bị khóa
    @Test
    public void testLockedOutUser() {
        driver.findElement(usernameField).sendKeys("locked_out_user");
        driver.findElement(passwordField).sendKeys("secret_sauce");
        driver.findElement(loginButton).click();

        WebElement error = wait.until(ExpectedConditions.visibilityOfElementLocated(errorMessage));
        Assert.assertTrue(error.getText().contains("Sorry, this user has been locked out"),
                "Không hiện thông báo locked out!");
    }

    // Kiểm tra UI
    @Test
    public void testUIElementsVisible() {
        Assert.assertTrue(driver.findElement(usernameField).isDisplayed(), "Textbox Username không hiển thị!");
        Assert.assertTrue(driver.findElement(passwordField).isDisplayed(), "Textbox Password không hiển thị!");
        Assert.assertTrue(driver.findElement(loginButton).isDisplayed(), "Button Login không hiển thị!");
    }

    @AfterMethod
    public void tearDown() throws InterruptedException {
        Thread.sleep(2000);
        if (driver != null) {
            driver.quit();
        }
    }

}
