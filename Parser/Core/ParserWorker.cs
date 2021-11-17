using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;

namespace Parser.Core
{
    class ParserWorker<T> where T : class
    {
        /*readonly*/ IParser<T> parser;
        IParserSettings parserSet;

        HtmlLoader loader;

        bool IsActive;

        #region Proper Lines
        public IParser<T> Parser
        {
            get
            {
                return parser;
            }

            set
            {
                parser = value;
            }
        }

        public IParserSettings Settings
        {
            get
            {
                return parserSet;
            }
            set
            {
                parserSet = value;
                loader = new HtmlLoader(value);
            }
        }

        public bool IsAvtive
        {
            get
            {
                return IsActive;
            }
            set
            {
                IsActive = value;
            }
        }
        #endregion


        public ParserWorker(IParser<T> parser, CurrencyParser.CurrencyParserSettings currencyParserSettings)
        {
            this.parser = parser;
        }

        public ParserWorker(IParser<T> parser, IParserSettings parserSet) : this(parser)
        {
            this.parserSet = parserSet;
        }

        public void Start()
        {
            Worker();
            IsActive = true;
        }

        public void Stop()
        {
            IsActive = false;
        }

        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleted;

        private async void Worker()
        {
            for(int i = parserSet.StartPoint; i < parserSet.EndPoint; ++i)
            {
                if (!IsActive)
                {
                    OnCompleted?.Invoke(this);
                    return;
                }

                var source = await loader.GetSourseByPageId(i);
                var domParser = new HtmlParser();

                var doc = await domParser.ParseDocumentAsync(source);
                 
                var result=parser.Parse(doc);
                OnNewData?.Invoke(this, result);
            }
            OnCompleted?.Invoke(this);
            IsActive = false;
        }
    }
}
