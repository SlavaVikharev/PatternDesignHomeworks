using System;
using Example_03.MailBuilder;
using Example_03.MailBuilderConveyor;
using System.Collections.Generic;
using System.Text;

namespace Example_03.MailBuilder
{
    class ClassicMailBuilder
    {
        private StringBuilder _body { get; }

        public ClassicMailBuilder() {
            _body = new StringBuilder();
        }

        public MailReceiversBuilder AppendLineToBody(string line) {
            _body.AppendLine(line);
            return new MailReceiversBuilder(_body);
        }

        public class MailReceiversBuilder
        {
            private StringBuilder _body { get; }
            private List<string> _receivers { get; }

            public MailReceiversBuilder(StringBuilder body) {
                _body = body;
                _receivers = new List<string>();
            }

            public FinalMailBuilder AddReceiver(string receiver) {
                _receivers.Add(receiver);
                return new FinalMailBuilder(_body, _receivers);
            }

            public MailReceiversBuilder AppendLineToBody(string line) {
                _body.AppendLine(line);
                return this;
            }
        }

        public class FinalMailBuilder
        {
            private string _subject { get; set; }
            private List<string> _receivers { get; }
            private StringBuilder _body { get; }

            public FinalMailBuilder(StringBuilder body, List<string> receivers) {
                _receivers = receivers;
                _body = body;
            }

            public FinalMailBuilder SetSubject(string subject) {
                _subject = subject;
                return this;
            }

            public FinalMailBuilder AddReceiver(string receiver) {
                _receivers.Add(receiver);
                return this;
            }

            public FinalMailBuilder AppendLineToBody(string line) {
                _body.AppendLine(line);
                return this;
            }

            public string Build() {
                return $"To: {string.Join(", ", _receivers)}\nSubject: {_subject}\n\n{_body.ToString()}";
            }
        }
    }
}

namespace Example_03.MailBuilderConveyor
{
    public interface IMailBuilder
    {
        string Build();
    }

    public interface IHaveNextBuilder<TNext> : IMailBuilder
    {
        TNext Next();
    }

    public abstract class MailBuilder : IMailBuilder
    {
        protected StringBuilder _prevMail;

        public MailBuilder(StringBuilder prevMail) {
            _prevMail = prevMail;
        }

        protected virtual StringBuilder UpdatedMail() {
            return _prevMail.AppendLine(ToString());
        }

        public virtual string Build() {
            return UpdatedMail().ToString();
        }
    }

    public class MailBuildersConveyor
    {
        public static MailReceiversBuilder Create() {
            var newMail = new StringBuilder();
            return new MailReceiversBuilder(newMail);
        }

        public class MailReceiversBuilder : MailBuilder, IHaveNextBuilder<MailSubjectBuilder>
        {
            private List<string> _receivers;

            public MailReceiversBuilder(StringBuilder prevMail) : base(prevMail) {
                _receivers = new List<string>();
            }

            public MailReceiversBuilder AddReceiver(string receiver) {
                _receivers.Add(receiver);
                return this;
            }

            public override string ToString() {
                return $"To: {string.Join(", ", _receivers)}";
            }

            public MailSubjectBuilder Next() {
                return new MailSubjectBuilder(UpdatedMail());
            }
        }

        public class MailSubjectBuilder : MailBuilder, IHaveNextBuilder<MailBodyBuilder>
        {
            private string _subject;

            public MailSubjectBuilder(StringBuilder prevMail) : base(prevMail) {
                _subject = "";
            }

            public MailSubjectBuilder SetSubject(string text) {
                _subject = text;
                return this;
            }

            public override string ToString() {
                return $"Subject: {_subject}";
            }

            public MailBodyBuilder Next() {
                return new MailBodyBuilder(UpdatedMail());
            }
        }

        public class MailBodyBuilder : MailBuilder
        {
            private StringBuilder _mailBody;

            public MailBodyBuilder(StringBuilder prevMail) : base(prevMail) {
                _mailBody = new StringBuilder();
            }

            public MailBodyBuilder AppendLineToBody(string text) {
                _mailBody.AppendLine(text);
                return this;
            }

            public override string ToString() {
                return _mailBody.ToString();
            }

            protected override StringBuilder UpdatedMail() {
                return _prevMail.AppendLine().AppendLine(ToString());
            }
        }
    }
}

namespace Example_03
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //RunMailBuilder();
            //RunClassicMailBuilder();

            Console.WriteLine();
            Console.ReadLine();
        }

        private static void RunClassicMailBuilder() {
            var mailBuilder = new ClassicMailBuilder();

            var res = mailBuilder
                .AppendLineToBody("Hello")
                .AppendLineToBody("123")
                .AddReceiver("512")
                .AddReceiver("123")
                .SetSubject("1231")
                .Build();

            Console.WriteLine(res);
        }

        private static void RunMailBuilder() {
            var mailConveyor = MailBuildersConveyor.Create();

            var res = mailConveyor
                .AddReceiver("213123")
                .AddReceiver("123")
                .Next()
                .SetSubject("231")
                .Next()
                .AppendLineToBody("213123")
                .AppendLineToBody("1231")
                .AppendLineToBody("213123")
                .Build();

            Console.WriteLine(res);
        }
    }
}
