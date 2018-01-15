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
        public string Gate { get; set; }
        public DateTime? EntryDate { get; set; }
        public string Token { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime FinishedDate { get; set; }
        public VoucherStatus CurrentStatus { get; set; }

        [Ignore]
        public bool IsVoucherPendingActivation => CurrentStatus == VoucherStatus.Active && ActivationDate > DateTime.Now;

        [Ignore]
        public bool IsVoucherActive => CurrentStatus == VoucherStatus.Active && ActivationDate <= DateTime.Now && !IsVoucherExpired;

        [Ignore]
        public bool IsVoucherUsed => CurrentStatus == VoucherStatus.Used;

        [Ignore]
        public bool IsVoucherExpired => ExpirationDate <= DateTime.Now && !IsVoucherUsed && !IsVoucherExchanged && !IsVoucherCancelled;

        [Ignore]
        public bool IsVoucherCancelled => CurrentStatus == VoucherStatus.Canceled;

        [Ignore]
        public bool IsVoucherExchanged => CurrentStatus == VoucherStatus.Exchanged;

        [Ignore]
        public string VoucherPendingActivationTextDate => $"{ActivationDate.ToString("dd/MM/yyyy")} às {ActivationDate.ToString("H:mm")}";

        [Ignore]
        public string VoucherExpiredTextDate => $"{ExpirationDate.ToString("dd/MM/yyyy")} às {ExpirationDate.ToString("H:mm")}";

        [Ignore]
        public string VoucherUsedTextDate => $"{EntryDate?.ToString("dd/MM/yyyy")} às {EntryDate?.ToString("H:mm")}";

        [Ignore]
        public string VoucherCancelledTextDate => $"{EntryDate?.ToString("dd/MM/yyyy")} às {EntryDate?.ToString("H:mm")}";

        [Ignore]
        public string VoucherExchangedTextDate => $"{EntryDate?.ToString("dd/MM/yyyy")} às {EntryDate?.ToString("H:mm")}";

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

        public void CheckVoucherStatus()
        {
            OnPropertyChanged("IsVoucherPendingActivation");
            OnPropertyChanged("IsVoucherActive");
            OnPropertyChanged("IsVoucherUsed");
            OnPropertyChanged("IsVoucherExpired");
            OnPropertyChanged("IsVoucherCancelled");
            OnPropertyChanged("IsVoucherExchanged");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
