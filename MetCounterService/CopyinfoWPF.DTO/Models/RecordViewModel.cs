using CopyinfoWPF.ORM;
using Prism.Mvvm;
using System;

namespace CopyinfoWPF.DTO.Models
{
    public class RecordViewModel : BindableBase
    {

        public RecordViewModel(int id, DatabaseType source)
        {
            Id = id;
            Source = source;
        }

        public int Id { get; }

        private string _textContent;
        public string TextContent
        {
            get => _textContent;
            set => SetProperty(ref _textContent, value);
        }

        public DatabaseType Source { get; }

        private string _serviceMan;
        public string ServiceMan
        {
            get { return _serviceMan; }
            set { SetProperty(ref _serviceMan, value); }
        }

        private DateTime? _dateTime;
        public DateTime? DateTime
        {
            get { return _dateTime; }
            set { SetProperty(ref _dateTime, value); }
        }

        private int _blackAndWhite;
        public int BlackAndWhite
        {
            get { return _blackAndWhite; }
            set { SetProperty(ref _blackAndWhite, value); }
        }

        private int _color;
        public int Color
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }

        private int _scan;

        public int Scan
        {
            get { return _scan; }
            set { SetProperty(ref _scan, value); }
        }

        private int? _emailContentId;
        public int? EmailContentId
        {
            get { return _emailContentId; }
            set { SetProperty(ref _emailContentId, value); }
        }

        private int? _htmlSourceId;
        public int? HtmlSourceId
        {
            get { return _htmlSourceId; }
            set { SetProperty(ref _htmlSourceId, value); }
        }
    }
}
