using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.Model;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;

namespace CopyinfoWPF.ViewModels
{
    public class ReportOverviewViewModel : BindableBase
    {

        public ReportOverviewViewModel() { }
        public ReportOverviewViewModel(RecordDetailsViewModel<EmailAttachment> recordDetailsViewModel)
        {
            TextContent = recordDetailsViewModel.Content;
            if (recordDetailsViewModel.List != null)
                EmailAttachments.AddRange(recordDetailsViewModel.List);
        }

        private string _textContent;
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
    }
}
