using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BasicApp.Voucher.Models;

namespace BasicApp.Voucher.Services
{
    public class VoucherServiceMock : IVoucherService
    {
        public VoucherServiceMock()
        {
        }

        public async Task<IEnumerable<Event>> GetEvents(bool updateFromServer)
        {
            return new List<Event>{
                new Event{
                    Name = "Teste",
                    Description1 = "Description1",
                    Date = DateTime.Now,
                    Store = new Store{Name = "Store"},
                    Image1 = "http://via.placeholder.com/500x500",
                    Image2 = "http://via.placeholder.com/1000x500",
                    Vouchers = new List<Models.Voucher>{
                        new Models.Voucher {
                            ClientCPFOwner = "123.123.123-12",
                            ClientNameOwner = "Teste",
                            ActivationDate = DateTime.Now,
                            ExpirationDate = DateTime.Now.AddHours(1),
                            Description1 = "Description1",
                            Token = "12345",
                            UserId = 1,
                            Id = 1,
                            FinishedDate = DateTime.Now.AddDays(1),
                            CurrentStatus = Enums.VoucherStatus.Active
                        },
                        new Models.Voucher {
                            ClientCPFOwner = "123.123.123-12",
                            ClientNameOwner = "Teste",
                            ActivationDate = DateTime.Now.AddHours(1),
                            ExpirationDate = DateTime.Now.AddHours(2),
                            Description1 = "Description1",
                            Token = "12345",
                            UserId = 1,
                            Id = 1,
                            FinishedDate = DateTime.Now.AddDays(1),
                            CurrentStatus = Enums.VoucherStatus.Active
                        },
                        new Models.Voucher {
                            ClientCPFOwner = "123.123.123-12",
                            ClientNameOwner = "Teste",
                            ActivationDate = DateTime.Now.AddHours(-1),
                            ExpirationDate = DateTime.Now,
                            Description1 = "Description1",
                            Token = "12345",
                            UserId = 1,
                            Id = 1,
                            FinishedDate = DateTime.Now.AddDays(1),
                            CurrentStatus = Enums.VoucherStatus.Active
                        },
                        new Models.Voucher {
                            ClientCPFOwner = "123.123.123-12",
                            ClientNameOwner = "Teste",
                            ActivationDate = DateTime.Now.AddHours(-1),
                            ExpirationDate = DateTime.Now,
                            Description1 = "Description1",
                            Token = "12345",
                            UserId = 1,
                            Id = 1,
                            EntryDate = DateTime.Now,
                            FinishedDate = DateTime.Now.AddDays(1),
                            CurrentStatus = Enums.VoucherStatus.Canceled
                        },
                        new Models.Voucher {
                            ClientCPFOwner = "123.123.123-12",
                            ClientNameOwner = "Teste",
                            ActivationDate = DateTime.Now.AddHours(-1),
                            ExpirationDate = DateTime.Now,
                            Description1 = "Description1",
                            Token = "12345",
                            UserId = 1,
                            Id = 1,
                            EntryDate = DateTime.Now,
                            FinishedDate = DateTime.Now.AddDays(1),
                            CurrentStatus = Enums.VoucherStatus.Exchanged
                        },
                        new Models.Voucher {
                            ClientCPFOwner = "123.123.123-12",
                            ClientNameOwner = "Teste",
                            ActivationDate = DateTime.Now.AddHours(-1),
                            ExpirationDate = DateTime.Now,
                            Description1 = "Description1",
                            Token = "12345",
                            UserId = 1,
                            Id = 1,
                            EntryDate = DateTime.Now,
                            FinishedDate = DateTime.Now.AddDays(1),
                            CurrentStatus = Enums.VoucherStatus.Used
                        }
                    }
                }
            };
        }
        public async Task<IEnumerable<Models.Voucher>> GetVouchers(int eventId)
        {
            return new List<Models.Voucher>{
                new Models.Voucher {
                    ClientCPFOwner = "123.123.123-12",
                    ClientNameOwner = "Teste",
                    ActivationDate = DateTime.Now,
                    ExpirationDate = DateTime.Now.AddHours(1),
                    Description1 = "Description1",
                    Token = "12345",
                    UserId = 1,
                    Id = 1,
                    FinishedDate = DateTime.Now.AddDays(1),
                    CurrentStatus = Enums.VoucherStatus.Active
                }
                //new Models.Voucher {
                //    ClientCPFOwner = "123.123.123-12",
                //    ClientNameOwner = "Teste",
                //    ActivationDate = DateTime.Now.AddHours(1),
                //    ExpirationDate = DateTime.Now.AddHours(2),
                //    Description1 = "Description1",
                //    Token = "12345",
                //    UserId = 1,
                //    Id = 1,
                //    FinishedDate = DateTime.Now.AddDays(1),
                //    CurrentStatus = Enums.VoucherStatus.Active
                //},
                //new Models.Voucher {
                //    ClientCPFOwner = "123.123.123-12",
                //    ClientNameOwner = "Teste",
                //    ActivationDate = DateTime.Now.AddHours(-1),
                //    ExpirationDate = DateTime.Now,
                //    Description1 = "Description1",
                //    Token = "12345",
                //    UserId = 1,
                //    Id = 1,
                //    FinishedDate = DateTime.Now.AddDays(1),
                //    CurrentStatus = Enums.VoucherStatus.Active
                //},
                //new Models.Voucher {
                //    ClientCPFOwner = "123.123.123-12",
                //    ClientNameOwner = "Teste",
                //    ActivationDate = DateTime.Now.AddHours(-1),
                //    ExpirationDate = DateTime.Now,
                //    Description1 = "Description1",
                //    Token = "12345",
                //    UserId = 1,
                //    Id = 1,
                //    EntryDate = DateTime.Now,
                //    FinishedDate = DateTime.Now.AddDays(1),
                //    CurrentStatus = Enums.VoucherStatus.Canceled
                //},
                //new Models.Voucher {
                //    ClientCPFOwner = "123.123.123-12",
                //    ClientNameOwner = "Teste",
                //    ActivationDate = DateTime.Now.AddHours(-1),
                //    ExpirationDate = DateTime.Now,
                //    Description1 = "Description1",
                //    Token = "12345",
                //    UserId = 1,
                //    Id = 1,
                //    EntryDate = DateTime.Now,
                //    FinishedDate = DateTime.Now.AddDays(1),
                //    CurrentStatus = Enums.VoucherStatus.Exchanged
                //},
                //new Models.Voucher {
                //    ClientCPFOwner = "123.123.123-12",
                //    ClientNameOwner = "Teste",
                //    ActivationDate = DateTime.Now.AddHours(-1),
                //    ExpirationDate = DateTime.Now,
                //    Description1 = "Description1",
                //    Token = "12345",
                //    UserId = 1,
                //    Id = 1,
                //    EntryDate = DateTime.Now,
                //    FinishedDate = DateTime.Now.AddDays(1),
                //    CurrentStatus = Enums.VoucherStatus.Used
                //}
            };
        }

        public Task<IEnumerable<VoucherQuickUpdate>> RefreshVouchers(IEnumerable<int> voucherIds)
        {
            throw new NotImplementedException();
        }
    }
}
