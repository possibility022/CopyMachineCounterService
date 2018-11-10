using CopyinfoWPF.Common;
using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.Interfaces.Formatters;
using CopyinfoWPF.Model;
using CopyinfoWPF.Workflows.Email;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;

namespace CopyinfoWPF.ViewModels
{
    public class ReportOverviewViewModel : BindableBase
    {

        IFormatter<EmailMessage> _emailFormatter;
        IFormatter<RecordViewModel> _recordFormatter;

        public ReportOverviewViewModel() { }

        public ReportOverviewViewModel(IFormatter<EmailMessage> emailFormatter, IFormatter<RecordViewModel> recordFormatter)
        {
            _emailFormatter = emailFormatter;
            _recordFormatter = recordFormatter;
        }

        private string _textContent = string.Empty;
        public string TextContent
        {
            get { return _textContent; }
            set { SetProperty(ref _textContent, value); }
        }

        private Visibility _listVisible;
        public Visibility ListVisible
        {
            get { return _listVisible; }
            set { SetProperty(ref _listVisible, value); }
        }

        private ObservableCollection<EmailAttachment> _emailAttachments = new ObservableCollection<EmailAttachment>();
        public ObservableCollection<EmailAttachment> EmailAttachments
        {
            get { return _emailAttachments; }
            set { SetProperty(ref _emailAttachments, value); }
        }

        public void ViewNewRecord(RecordViewModel recordViewModel)
        {
            if (recordViewModel == null)
            {
                TextContent = string.Empty;
                ListVisible = Visibility.Hidden;
                return;
            }

            switch(recordViewModel.Source)
            {
                case ORM.DatabaseType.CounterService:
                    TextContent = _emailFormatter
                        .GetText(new EmailMessage(recordViewModel.BinaryContent))
                        .ToString();
                    break;

                case ORM.DatabaseType.Assystent:
                    TextContent = _recordFormatter
                        .GetText(recordViewModel)
                        .ToString();
                    break;

                default:
                    Log.LogMessage($"Error datasource [{recordViewModel.Source}] is not supported");
                    break;
            }
        }

        public void OnRecordSelected(object sender, RecordViewModel e)
        {
            ViewNewRecord(e);
        }
    }
}
