using System.Collections.Generic;
using System.Linq;

namespace WebUI.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public bool Selected { get; set; }
        public string Roles { get; set; }
        public bool isVisible { get; set; }
        
        public IEnumerable<Menu> SubMenus { get; set; }
    }
    
    public class MyMenu
    {
        public IEnumerable<Menu> NavMenu { get; set; }
        public IEnumerable<Menu> SubMenu { get; set; }
        public string Cust { get; set; }
        public string UserId { get; set; }
        public int ContractId { get; set; }
        public IList<string> Roles { get; set; }
        public decimal sm { get; set; }
        public int CustId { get; set; }
        public MyMenu(IList<string> roles, string cust)
        {
            Cust = cust;
            Roles = roles;
            NavMenu = new[]
            {
            new Menu
            {
                Id = 1,
                Title = "Профиль",
                Action = "Index",
                Controller = "Home",
                Selected = true,
                Roles="All",
                SubMenus=new[]
                {
                    new Menu
                    {
                        Id = 1,
                        Title = "Реквизиты",
                        Action = "Index",
                        Controller = "Home",
                        Roles="All",
                        Selected = true,
                    },
                    new Menu
                    {
                        Id = 2,
                        Title = "Изменить пароль",
                        Action = "ChangePassword",
                        Controller = "Account",
                        Roles="All",
                        Selected = false,
                    },
                    new Menu
                    {
                        Id = 3,
                        Title = "Обращения, жалобы, предложения",
                        Action = "Index",
                        Controller = "Support",
                        Roles="All",
                        Selected = true,
                     },
                    new Menu
                    {
                        Id = 4,
                        Title = "Рассылка документов",
                        Action = "Index",
                        Controller = "CustomInfo",
                        Selected = true,
                   }
                }
            },
            new Menu
            {
                Id = 2,
                Title = "Сведения",
                Action = "Index",
                Controller = "Docs",
                Selected = false,
                SubMenus=new[]
                {
                    new Menu
                    {
                        Id = 1,
                        Title = "Действующие доверенности",
                        Action = "Index",
                        Controller = "Docs",
                        Selected = true,
                    },
                  new Menu
                    {
                        Id = 4,
                        Title = "Доверенный транспорт",
                        Action = "Index",
                        Controller = "TrustTecs",
                        Selected = true,
                    },
                    new Menu
                    {
                        Id = 2,
                        Title = "Договоры",
                        Action = "Contract",
                        Controller = "Docs",
                        Selected = false,
                    },
                    new Menu
                    {
                        Id = 3,
                        Title = "Счет-фактура",
                        Action = "SchFact",
                        Controller = "Docs",
                        Selected = true,
                    },
                    new Menu
                    {
                        Id = 5,
                        Title = "Контакты",
                        Action = "Index",
                        Controller = "People",
                        Selected = true,
                    },
                    new Menu
                    {
                        Id = 6,
                        Title = "Адреса",
                        Action = "Index",
                        Controller = "Adres",
                        Selected = true,
                    }
                }
            },
            new Menu
            {
                Id = 3,
                Title = "Отгрузка и оплата",
                Action = "Balance",
                Controller = "Balances",
                Selected = false,
                SubMenus = new[]
                {
                    new Menu
                    {
                        Id = 1,
                        Title = "Взаиморасчеты",
                        Action = "Balance",
                        Controller = "Balances",
                        Selected = false,
                    },
                    new Menu
                    {
                        Id = 2,
                        Title = "Реестр отгруженной продукции",
                        Action = "Index",
                        Controller = "Balances",
                        Selected = false,
                    },
                      new Menu
                    {
                        Id = 3,
                        Title = "Текущее выполнение",
                        Action = "Index",
                        Controller = "Sale",
                        Selected = false,
                    },
                      new Menu
                    {
                        Id = 4,
                        Title = "Отправленные на погрузку",
                        Action = "Loadering",
                        Controller = "Sale",
                        Selected = false,
                    }
                }
            },

            new Menu
            {
                Id = 4,
                Title = "Заявки",
                Action = "Order",
                Controller = "Order",
                Selected = false,
                SubMenus=new[]
                {
                    new Menu
                    {
                        Id = 1,
                        Title = "Заказ продукции",
                        Action = "Order",
                        Controller = "Order",
                        Selected = true,
                     },
                      new Menu
                    {
                        Id = 2,
                        Title = "Заказ счета",
                        Action = "Invoice",
                        Controller = "Order",
                        Selected = false,
                    }
                }
             },
            
            new Menu
            {
                Id = 6,
                Title = "Администрирование",
                Action = "Index",
                Controller = "Admin",
                Roles="Admin",
                Selected = false,
                SubMenus = new[]
                {
                    new Menu
                    {
                        Id = 1,
                        Title = "Администрирование",
                        Action = "Index",
                        Controller = "Admin",
                        Roles="Admin",
                        Selected = true,
                     },
                    new Menu
                    {
                        Id = 2,
                        Title = "Регистрация",
                        Action = "Register",
                        Controller = "Admin",
                        Roles="Admin",
                        Selected = true,
                     },
                    new Menu
                    {
                        Id = 3,
                        Title = "Продукция",
                        Action = "Good",
                        Controller = "Admin",
                        Roles="Admin",
                        Selected = true,
                     },
                }
              }
            };
        }

        public bool isRoles(string mroles)
        {
            if(mroles=="All")
                return true;
            string[] rl = mroles.Split();
            foreach (var st in rl)
            {
                if (Roles.Any(w => w.Contains(st)))
                    return true;                
            }
            return false;
        }

        public void ChangeSelected(int id=1, int subid=1)
        {
            //Работа с меню
            Menu menu = (from m in NavMenu
                         where m.Selected == true
                         select m)
                        .First();

            menu.Selected = false;
            foreach (var link in NavMenu)
            {
                if (link.Roles == null)
                    link.isVisible = true;
                else
                    link.isVisible = isRoles(link.Roles);

                if (link.Id == id)
                {
                    link.Selected = true;
                    if (link.SubMenus!=null)
                        foreach (var sub in link.SubMenus)
                        {
                            if (sub.Id == subid)
                            {
                                sub.Selected = true;
                            }
                            else
                            {
                                sub.Selected = false;
                            }
                        }
                }
                else
                {
                    link.Selected = false;
                }
            }
        }
    }
}