﻿namespace FreeCourse.Web.Models.Basket
{
    public class BasketViewModel
    {
        public string UserId { get; set; } = string.Empty;

        public string DiscountCode { get; set; } = string.Empty;

        public int? DiscountRate { get; set; }

        private List<BasketItemViewModel> _basketItems { get; set; }

        public List<BasketItemViewModel> BasketItems
        {
            get
            {
                if (HasDiscount())
                {
                    _basketItems.ForEach(x =>
                    {
                        var discountPrice = x.Price * ((decimal)DiscountRate.Value / 100);
                        x.AppliedDiscount(Math.Round(x.Price - discountPrice, 2));
                    });
                }
                return _basketItems;
            }
            set
            {
                _basketItems = value;
            }
        }

        public decimal TotalPrice { get => _basketItems.Sum(x => x.Quantity * x.GetCurrentPrice()); }

        public bool HasDiscount() => !string.IsNullOrEmpty(DiscountCode);

        public BasketViewModel()
        {
            BasketItems = new List<BasketItemViewModel>();
        }
    }
}
