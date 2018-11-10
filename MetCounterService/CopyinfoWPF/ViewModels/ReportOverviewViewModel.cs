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

        public ReportOverviewViewModel() { }

        public ReportOverviewViewModel(IFormatter<EmailMessage> emailFormatter)
        {
            _emailFormatter = emailFormatter;
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

        public void ViewNewRecord(RecordViewModel recordDetailsViewModel)
        {
            if (recordDetailsViewModel == null)
            {
                TextContent = string.Empty;
                ListVisible = Visibility.Hidden;
                return;
            }

            switch(recordDetailsViewModel.Source)
            {
                case ORM.DatabaseType.CounterService:
                    TextContent = _emailFormatter
                        .GetText(new EmailMessage(recordDetailsViewModel.BinaryContent))
                        .ToString();
                    break;

                case ORM.DatabaseType.Assystent:
                    TextContent = recordDetailsViewModel.TextContent;
                    break;

                default:
                    Log.LogMessage($"Error datasource [{recordDetailsViewModel.Source}] is not supported");
                    break;
            }
        }

        public void OnRecordSelected(object sender, RecordViewModel e)
        {
            ViewNewRecord(e);
        }
    }
}
