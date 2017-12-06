using System;
using Autofac;
using BasicApp.Voucher.Services;
using BasicApp.Voucher.ViewModels;
using BasicApp.Voucher.Views;
using Prism.Autofac;
using Prism.Modularity;

namespace BasicApp.Voucher
{
    public class VoucherModule : IModule
    {
        public VoucherModule()
        {
        }

        public void Initialize()
        {
        }

        public static void Initialize(ContainerBuilder builder)/* See https://dansiegel.net/post/2017/08/02/breaking-changes-for-prism-autofac-users*/
        {
            builder.RegisterTypeForNavigation<EventListPage, EventListViewModel>("EventList");
            builder.RegisterTypeForNavigation<EventVoucherListPage, EventVoucherListViewModel>("EventVoucherList");
            builder.RegisterType<VoucherService>().As<IVoucherService>();
        }
    }
}
