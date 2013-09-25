using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using AppointmentBusiness.BO;
using AppointmentSystem.Settings.BusinessLayer;
using Log;

namespace AutoSendSms
{
    public partial class SmsService : ServiceBase
    {
        private Thread _smsThread;
        private int _timeInterval;

        public SmsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (!int.TryParse(ConfigurationManager.AppSettings.Get("TimeInterval"), out _timeInterval))
                Stop();
            _timeInterval = _timeInterval * 60000;

            _smsThread = new Thread(RunSms) { IsBackground = true };
            _smsThread.Start();
        }

        protected override void OnStop()
        {
            if (_smsThread.IsAlive)
            {
                _smsThread.Abort();
            }
        }

        private void RunSms()
        {
            var now = DateTime.Now;
            SingletonLogger.Instance.Debug(String.Format("RunSms time: {0:HH:mm MM/dd/yyyy}--------Start--------Time Interval: {1}", now, _timeInterval));
            BoFactory.SmsBO.CheckAndSendSms();
            SingletonLogger.Instance.Debug(String.Format("RunSms time: {0:HH:mm MM/dd/yyyy}--------End", now));
            Thread.Sleep(_timeInterval);
            RunSms();
        }
    }
}
