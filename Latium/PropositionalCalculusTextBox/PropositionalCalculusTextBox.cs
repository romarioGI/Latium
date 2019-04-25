using System.Text;
using System.Windows.Controls;

namespace PropositionalCalculusTextBox
{
    public class PropositionalCalculusTextBox : TextBox
    {
        private static char[] SubNums = {'₀', '₁', '₂', '₃', '₄', '₅', '₆', '₇', '₈', '₉'};
        private const char ImplicationSymbol = '→';
        private const char ConjunctionSymbol = '&';
        private const char DisjunctionSymbol = '∨';
        private const char NegationSymbol = '¬';

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            var selInd = SelectionStart;
            base.OnTextChanged(e);
            InputTransform();
            SelectionStart = selInd;
        }

        private void InputTransform()
        {
            var text = new StringBuilder(base.Text);
            for (var i = 0; i < text.Length; i++)
            {
                if (char.IsDigit(text[i]))
                {
                    if (i == 0)
                        continue;

                    if (char.IsLetter(text[i - 1]) || (text[i - 1] >= SubNums[0] && text[i - 1] <= SubNums[9]))
                        text[i] = SubNums[text[i] - '0'];
                    continue;
                }

                switch (text[i])
                {
                    case '>':
                        text[i] = ImplicationSymbol;
                        continue;
                    case '&':
                        text[i] = ConjunctionSymbol;
                        continue;
                    case '|':
                        text[i] = DisjunctionSymbol;
                        continue;
                    case '-':
                        text[i] = NegationSymbol;
                        continue;
                }
            }

            Text = text.ToString();
        }

        private string OutPutTransform()
        {
            var text = new StringBuilder(base.Text);
            for (var i = 0; i < text.Length; i++)
                if (text[i] >= SubNums[0] && text[i] <= SubNums[9])
                    text[i] = (char) (text[i] - SubNums[0] + '0');

            return text.ToString();
        }

        public string GetTextWithoutSubs()
        {
            return OutPutTransform();
        }

        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }
    }
}
