using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using static igor.FakeUer;
using AutoItX3Lib;
using Google.Cloud.Firestore;
using System.IO;

namespace igor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IWebDriver cd;
        private iProxy iproxy;
        bool useProxy = true;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_LoadedAsync;
            Closing += MainWindow_Closing;
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            cd.Dispose();
            if (cd != null)
            {
                cd.Quit();
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            cd.Dispose();
            if (cd!=null)
            {
                cd.Quit();
            }
            Environment.Exit(Environment.ExitCode);
        }

        private async void MainWindow_LoadedAsync(object sender, RoutedEventArgs e)
        {


            // Result u= await GetRandomUserNameAsync();
            //SignUpSequence(u);
            // LoginSequence();
            // CheckLock();
            //NurtureSequence();
            //FollowSequence();
            //ScrappingSequence();
            //Application.Current.Shutdown();
            //for (int i = 0; i < 20; i++)
            //{
            //    Result u = await GetRandomUserNameAsync();
            //    SignUpSequence(u);
            //    if (CheckLock())
            //    {
            //       return;
            //    }
            //    NurtureSequence();
            //    FollowSequence();
            //}

            //InitiateChrome(new iProxy() {ip= "199.247.15.159",port= "15447",username= "LMR0wv",password="5AVyRF" });



            }

        private void UpdateStatus(string message)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                StatusSheet.Text += Environment.NewLine + message;
            }));
          
        }

        private iProxy GetRandomProxy()
        {
            var rand = new Random();
            var file = File.ReadAllLines(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "proxylist.txt"));
             var pro = file[rand.Next(file.Length)];
            iProxy ip = new iProxy();
            ip.ip = pro.Split(':')[0];
            ip.port = pro.Split(':')[1];
            UpdateStatus("New Proxy Generated:");
            UpdateStatus(ip.ip+":"+ip.port);
            iproxy = ip;
            return ip ;
        }

        private string GetRandomUserAgent()
        {
            var rand = new Random();
            var file = File.ReadAllLines(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "agents.txt"));
            
            return file[rand.Next(file.Length)];
        }

        class iProxy
        {
            public string ip { get; set; }
            public string port { get; set; }
            public string username { get; set; }
            public string password { get; set; }
        }

        private bool CheckLock()
        {
            cd.Navigate().GoToUrl(@"https://www.instagram.com/explore/");
            if (CheckIfAvailable(By.XPath("//h2[text()='Add Your Phone Number']")))
            {
                return true;
            }
            return false;
            
            
        }

        private void NurtureSequence()
        {
            UploadProfilePhoto();
            UploadPhoto();
            UploadPhoto();
            UploadPhoto();
            UploadPhoto();
            UploadPhoto();
            UploadPhoto();
            UploadPhoto();
            UploadPhoto();
            UploadPhoto();
        }

        private void UploadProfilePhoto()
        {
            cd.Navigate().GoToUrl(@"https://www.instagram.com/explore/");
            WaitLoad(By.XPath("//span[contains(@aria-label,'Profile')]")).Click();
            WaitLoad(By.XPath("//button[contains(@title,'Add a profile photo')]")).Click();
            AutoItX3 au = new AutoItX3();
            au.WinActivate("Open");
            Thread.Sleep(1000);
            au.Send(GetRandomProfilePic());
            Thread.Sleep(500);
            au.Send("{ENTER}");
            WaitLoad(By.XPath("//button[text()='Save']")).Click();
            Thread.Sleep(2000);
        }

        private void UploadPhoto()
        {
            cd.Navigate().GoToUrl(@"https://www.instagram.com/explore/");
            WaitLoad(By.XPath("//span[contains(@aria-label,'New Post')]")).Click();
            AutoItX3 au = new AutoItX3();
            au.WinActivate("Open");
            Thread.Sleep(1000);
            au.Send(GetRandomPhoto());
            Thread.Sleep(500);
            au.Send("{ENTER}");
            WaitLoad(By.XPath("//button[text()='Next']")).Click();
            WaitLoad(By.XPath("//textarea[contains(@placeholder,'Write a caption')]")).SendKeys("...");
            WaitLoad(By.XPath("//button[text()='Share']")).Click();
            WaitLoad(By.XPath("//p[text()='Your photo was posted.']"));
            Thread.Sleep(2000);

        }

        private string GetRandomPhoto()
        {            
            var rand = new Random();
            var files = Directory.GetFiles(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),"Images\\"), "*.jpg");
            return files[rand.Next(files.Length)];
        }

        private string GetRandomProfilePic()
        {
            var rand = new Random();
            var files = Directory.GetFiles(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "ProfilePics\\"), "*.jpg");
            return files[rand.Next(files.Length)];
        }

        private void ScrappingSequence()
        {
        

            string hashtag = "fashion";
            cd.Navigate().GoToUrl(@"https://www.instagram.com/explore/tags/"+hashtag+"/");
            WaitLoad(By.XPath("//a[contains(@href,'/p/')]")).Click();
            //WaitLoad(By.XPath("//h2/a[contains(@href,'/']")).Click();
        }

        private void FollowSequence()
        {
            string username = "i.d.k.w.i.d";
            cd.Navigate().GoToUrl(@" https://www.instagram.com/explore/");
       
           WaitLoad(By.XPath("//input[@placeholder='Search']")).SendKeys(username);
            WaitLoad(By.XPath("//a[contains(@href,'"+username+"')]")).Click();
            WaitLoad(By.XPath("//h1[contains(@title,'" + username + "')]"));
            if (CheckIfAvailable(By.XPath("//button[text()='Following']")))
            {
                MessageBox.Show("Already Following");
            }
            else
            {
                WaitLoad(By.XPath("//button[text()='Follow']")).Click();
            };

        }

        private bool CheckIfAvailable(By by)
        {
            for (int i = 0; i < 20; i++)
            {
                try
                {
                    cd.FindElement(by);
                    return true;
                }
                catch (Exception)
                {
                    Thread.Sleep(500);
                    i++;
                }
            }
            return false;
        }

        private void LoginSequence()
        {
            InitiateChrome(null);
            cd.Navigate().GoToUrl(@"https://www.instagram.com/accounts/login/?source=auth_switcher");

            WaitLoad(By.Name("username")).SendKeys("martial_roy");
            cd.FindElement(By.Name("password")).SendKeys("tx9pcydwd9dwd9");
            cd.FindElement(By.XPath("//button[text()=\"Log in\"]")).Click();
            ClickIfAvailable(By.XPath("//a[text()=\"Not Now\"]"));
            ClickIfAvailable(By.XPath("//a[text()=\"Not Now\"]"));
            ClickIfAvailable(By.XPath("//button[text()=\"Cancel\"]"));
            ClickIfAvailable(By.XPath("//a[text()=\"Not Now\"]"));

        }

        private void ClickIfAvailable(By by)
        {
           
            for (int i = 0; i < 20; i++)
            {
                try
                {
                    cd.FindElement(by).Click();
                    return;
                }
                catch (Exception)
                {
                    Thread.Sleep(200);
                    i++;
                }
            }
          
        }

        private async Task<Result> GetRandomUserNameAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string resp= await client.GetStringAsync(@"https://randomuser.me/api/");
                return JsonConvert.DeserializeObject<User>(resp).results[0]; ;                
            }
        }

        private void LoginToEmail(Result u)
        {
            InitiateChrome(null);
            cd.Navigate().GoToUrl(@"https://shitmail.me");
            cd.FindElement(By.Id("mail_start")).SendKeys(Keys.Control + "a");
            cd.FindElement(By.Id("mail_start")).SendKeys(u.name.first+"_"+ u.name.last);
            cd.FindElement(By.Id("read_button")).Click();
        }

        private void InitiateChrome(iProxy proxy)
        {
            
            UpdateStatus("Initiating WebDriver");
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--user-agent="+GetRandomUserAgent());

            if (useProxy)
            {
                options.AddExtension(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "extensions\\ext.crx"));
               // options.AddArguments("--proxy-server=socks4://" + proxy.ip + ":" + proxy.port);
            }
            cd = new ChromeDriver(options);

            if (useProxy)
            {
                WaitLoad(By.XPath("//input[@id='http-host']")).SendKeys(proxy.ip);
                WaitLoad(By.XPath("//input[@id='http-port']")).SendKeys(proxy.port);
                WaitLoad(By.XPath("//input[@id='https-host']")).SendKeys(proxy.ip);
                WaitLoad(By.XPath("//input[@id='https-port']")).SendKeys(proxy.port);
                WaitLoad(By.XPath("//input[@id='socks-host']")).SendKeys(proxy.ip);
                WaitLoad(By.XPath("//input[@id='socks-port']")).SendKeys(proxy.port);
                WaitLoad(By.XPath("//input[@id='username']")).SendKeys(proxy.username);
                WaitLoad(By.XPath("//input[@id='password']")).SendKeys(proxy.password);
                WaitLoad(By.XPath("//span[text()='SOCKS5']")).Click();
                cd.Navigate().GoToUrl("chrome-extension://mnloefcpaepkpmhaoipjkpikbnkmbnic/popup.html");
                WaitLoad(By.XPath("//span[@id='socks5']")).Click();
                

            }


        }

      

        private void SignUpSequence(Result u)
        {

            //GetRandomProxy()
            u.email = u.name.first + "_" + u.name.last +DateTime.Now.DayOfYear+ "@shitmail.me";
            InitiateChrome(new iProxy() { ip = "45.77.91.16", port = "12264", username = "e878Nt", password = "V8uujw" });
            //InitiateChrome(null);
            cd.Navigate().GoToUrl(@"http://instagram.com");
            WaitLoad(By.XPath("//button[text()='Sign up with email or phone number']")).Click();
            WaitLoad(By.XPath("//span[text()='Email']")).Click();
            WaitLoad(By.XPath("//input[@name='email']")).SendKeys(u.email);
            WaitLoad(By.XPath("//button[text()='Next']")).Click();
            WaitLoad(By.XPath("//input[@name='fullName']")).SendKeys(u.name.first + " " + u.name.last);
            WaitLoad(By.XPath("//input[@name='password']")).SendKeys("tx9pcydwd9dwd9");
            WaitLoad(By.XPath("//button[text()='Next']")).Click();
            WaitLoad(By.XPath("//h1[contains(text(),'Welcome to Instagram')]"));
            Thread.Sleep(4000);
            
            WaitLoad(By.XPath("//button[text()='Next']")).Click();
            if (CheckIfAvailable(By.XPath("//h3[text()=\"Error\"]")))
            {
                return;
            }
            if (CheckIfAvailable(By.XPath("//header[text()=\"Are You 18 Years or Older ?\"]")))
            {
                ClickIfAvailable(By.XPath("//input[@value='above_18']"));
                WaitLoad(By.XPath("//button[text()='Next']")).Click();

            }
            ClickIfAvailable(By.XPath("//button[text()='Skip']"));
            ClickIfAvailable(By.XPath("//a[text()=\"Not Now\"]"));
            //ClickIfAvailable(By.XPath("//button[text()='Skip']"));
            //ClickIfAvailable(By.XPath("//button[text()='Skip']"));
            //ClickIfAvailable(By.XPath("//button[text()='Skip']"));
            //ClickIfAvailable(By.XPath("//a[text()=\"Not Now\"]"));
            //ClickIfAvailable(By.XPath("//button[text()=\"Cancel\"]"));
            //ClickIfAvailable(By.XPath("//a[text()=\"Not Now\"]"));
           

           
            FirestoreDb db = FirestoreDb.Create("igor-harvester");
            CollectionReference collection = db.Collection("bots");
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "FirstName", u.name.first },
                { "LastName",  u.name.last },
                { "DateOfBirth",  u.dob.date },
                { "Email",  u.email },
                { "Gender",  u.gender },
                { "ProfilePic",  u.picture.medium },
                 { "Registerd",  false },
                 { "CreationTime",  DateTime.Now.ToLongDateString() },
                // { "ProxyIP",  iproxy.ip },
                 // { "ProxyPort",  iproxy.port },




    };
            Google.Cloud.Firestore.DocumentReference document = collection.AddAsync(user).Result;

            Dictionary<string, object> update = new Dictionary<string, object>
            {
               { "Registerd",  true },
            };
            document.UpdateAsync(update);
          



        }

       

        private IWebElement WaitLoad(By by)
        {
            IWebElement result = null;
            for (int i = 0; i < 1000; i++)
            {
                try
                {
                    result = cd.FindElement(by);
                    return result;
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                    Thread.Sleep(500);
                    i++;
                }

            }
            return result;
        }


        private void btn_CreateNewUser(object sender, RoutedEventArgs e)
        {

            Thread t = new Thread(() =>
            {
                useProxy = false;
                Result u = GetRandomUserNameAsync().Result;
                SignUpSequence(u);
                if (CheckLock())
                {
                    return;
                }
                NurtureSequence();
                FollowSequence();
            }

             );

            t.Start();


        }

        private void btn_LoginAs(object sender, RoutedEventArgs e)
        {

        }

        private void btn_LognAsRandomUser(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(() =>
            {
                useProxy = true;
                Result u = GetRandomUserNameAsync().Result;
                SignUpSequence(u);
                if (CheckLock())
                {
                    return;
                }
                NurtureSequence();
                FollowSequence();
            }

                );

            t.Start();
            
               
            


        }
    }
}
