using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using BasicApp.Database;
using BasicApp.Policies.Exceptions;
using BasicApp.Totp;
using SQLite;
using Xamarin.Forms;
using ZXing;
using ZXing.Common;
using ZXing.Mobile;
using BasicApp.Voucher.Enums;

namespace BasicApp.Voucher.Models
{
    public class Voucher : IEntity, INotifyPropertyChanged
    {
        public Voucher()
        {
        }

        [PrimaryKey]
        public int Id { get; set; }
        public int EventId { get; set; }
        public string ClientCPFOwner { get; set; }
        public string ClientNameOwner { get; set; }
        public string ClientOrderId { get; set; }
        public string ClientTicketId { get; set; }
        public string ClientEventId { get; set; }
        public int UserId { get; set; }
        public string ClientUserId { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string Description3 { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public VoucherStatus CurrentStatus { get; set; }


        private ImageSource _qrcode;

        [Ignore]
        public ImageSource QrCode
        {
            get => _qrcode; set
            {
                _qrcode = value;
                OnPropertyChanged("QrCode");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
