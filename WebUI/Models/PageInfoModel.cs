using System;

namespace WebUI.Models
{
    public class PageInfo
    {
        public int pageNum { get; set; }        // Номер выбранной страницы
        public int itemsCount { get; set; }     // Общее количество элементов 
        public int pageSize { get; set; }       // Количество элементов на странице
        public int vsblPagesCount { get; set; } // Количество отображаемных страниц - диапазон    
    }
}