using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VarunGrapeCityTest
{
    public partial class VarunTipCalculator : Form
    {
        public VarunTipCalculator()
        {
            InitializeComponent();
        }

        // Method to Reset all the fields and set them to their initial values when an invalid move occurs.
        private void ResetFields()
        {
            TextBox1.Text = "";
            Label1.Text = "0%";
            Label2.Text = "1";
            double num = 0.0;
            TipPerPersonAmountLabel.Text = num.ToString("C");
            TotalPerPersonAmountLabel.Text = num.ToString("C");
        }

        // Method to check whether the user has entered any value in Bill box or not.
        // If box is empty, user is given a message that Bill amount cannot be left blank.
        private bool isBillBlank()
        {
            if (TextBox1.Text == "")
            {
                ResetFields();
                MessageBox.Show("Bill amount cannot be blank");
                return true;
            }
            return false;
        }


        // Method to show error message when bill amount entered is less than or equal to 0.
        private void printBillRangeMsg()
        {
            ResetFields();
            MessageBox.Show("Bill cannot be less than or equal to 0");
        }

        // Method to show error message when bill amount entered is not in proper for-mat.
        // For example, any alphabet or special character entered instead of numbers.
        private void printBillInvalidMsg()
        {
            ResetFields();
            MessageBox.Show("Bill amount invalid");
        }

        // Method to calculate the Tip per person and display it.
        private void tipPerPerson(double billAmount)
        {
            double tipPerPerson, intermediateResult;
            int noOfPeople = getNoOfPeople();
            int tipPercent = getTipPercent();

            intermediateResult = (billAmount * tipPercent) / 100;
            tipPerPerson = intermediateResult / noOfPeople;
            TipPerPersonAmountLabel.Text = tipPerPerson.ToString("C");
        }

        // Method to calculate the Total per person and display it.
        private void totalPerPerson(double billAmount)
        {
            double totalPerPerson, intermediateResult;
            int noOfPeople = getNoOfPeople();
            int tipPercent = getTipPercent();

            intermediateResult = (billAmount * tipPercent) / 100;
            totalPerPerson = (billAmount + intermediateResult) / noOfPeople;
            TotalPerPersonAmountLabel.Text = totalPerPerson.ToString("C");
        }

        // Method to retrieve the Tip Percentage entered from its respective box and returns
        // it to the calling method.
        // This method converts string input of Tip percentage into its equivalent nu-meric value
        // and returns that value.
        private int getTipPercent()
        {
            string textOfTipBox = Label1.Text;
            string numInTipBox = "";
            int val = 0;

            for (int i = 0; i < textOfTipBox.Length; i++)
            {
                if (Char.IsDigit(textOfTipBox[i]))
                {
                    numInTipBox += textOfTipBox[i];
                }
                else
                {
                    break;
                }
            }

            val = int.Parse(numInTipBox);
            return val;
        }

        // Method to retrieve the Number of people entered from its respective box and returns
        // it to the calling method.
        // This method converts string input of Number of people into its equivalent numeric value
        // and returns that value.
        private int getNoOfPeople()
        {
            int val = Convert.ToInt32(Label2.Text);
            return val;
        }


        // This method gets invoked when user presses the + button for increasing the Tip
        // percentage by 1% (each time).
        // It first checks the bill amount is not empty by calling the method "is-BillBlank()".
        // Then it converts and simultaneously checks for any invalid amount entered.
        private void incrementTipButtonClicked(object sender, EventArgs e)
        {
            if (!isBillBlank())
            {
                try
                {
                    double billAmount = Convert.ToDouble(TextBox1.Text);

                    // Checking if Bill is less than or equal to 0, then print that respective error message.
                    if (billAmount <= 0)
                    {
                        printBillRangeMsg();
                    }
                    else
                    {
                        int val = getTipPercent();
                        ++val;
                        Label1.Text = val.ToString() + "%";

                        // Tip increased by 1% and calculating new Tip per person and Total per person with new value
                        tipPerPerson(billAmount);
                        totalPerPerson(billAmount);
                    }
                }
                catch (FormatException exc)
                {
                    // Handling exception in case any invalid amount is entered in Bill box
                    printBillInvalidMsg();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Some error occurred. Try again!");
                    ResetFields();
                }
            }
        }

        // This method gets invoked when user presses the - button for decreasing the Tip
        // percentage by 1% (each time).
        // It first checks the bill amount is not empty by calling the method "is-BillBlank()".
        // Then it converts and simultaneously checks for any invalid amount entered.
        protected void decrementTipButtonClicked(object sender, EventArgs e)
        {
            if (!isBillBlank())
            {
                try
                {
                    double billAmount = Convert.ToDouble(TextBox1.Text);

                    // Checking if Bill is less than or equal to 0, then print that respective error message.
                    if (billAmount <= 0)
                    {
                        printBillRangeMsg();
                    }
                    else
                    {
                        int val = getTipPercent();

                        // Retrieving current value of Tip % before decrementing and if it is 0, then decrementing further
                        // is not possible and showing respective error message.
                        if (val == 0)
                        {
                            MessageBox.Show("Tip cannot be less than 0% !");
                            ResetFields();
                        }
                        else
                        {
                            --val;
                            Label1.Text = val.ToString() + "%";

                            // Tip decreased by 1% and calculating new Tip per person and Total per person with new value
                            tipPerPerson(billAmount);
                            totalPerPerson(billAmount);
                        }
                    }
                }
                catch (FormatException exc)
                {
                    // Handling exception in case any invalid amount is entered in Bill box
                    printBillInvalidMsg();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Some error occurred. Try again!");
                    ResetFields();
                }
            }
        }


        // This method gets invoked when user presses the + button for increasing the Number
        // of people by 1 (each time).
        // It first checks the bill amount is not empty by calling the method "is-BillBlank()".
        // Then it converts and simultaneously checks for any invalid amount entered.
        protected void incrementPeopleButtonClicked(object sender, EventArgs e)
        {
            if (!isBillBlank())
            {
                try
                {
                    double billAmount = Convert.ToDouble(TextBox1.Text);

                    // Checking if Bill is less than or equal to 0, then print that respective error message.
                    if (billAmount <= 0)
                    {
                        printBillRangeMsg();
                    }
                    else
                    {
                        int val = getNoOfPeople();
                        ++val;
                        Label2.Text = val.ToString();

                        // Number of people increased by 1 and calculating new Tip per person and Total per person with new value
                        tipPerPerson(billAmount);
                        totalPerPerson(billAmount);
                    }
                }
                catch (FormatException exc)
                {
                    // Handling exception in case any invalid amount is entered in Bill box
                    printBillInvalidMsg();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Some error occurred. Try again!");
                    ResetFields();
                }
            }
        }

        // This method gets invoked when user presses the - button for decreasing the Number
        // of people by 1 (each time).
        // It first checks the bill amount is not empty by calling the method "is-BillBlank()".
        // Then it converts and simultaneously checks for any invalid amount entered.
        protected void decrementPeopleButtonClicked(object sender, EventArgs e)
        {
            if (!isBillBlank())
            {
                try
                {
                    double billAmount = Convert.ToDouble(TextBox1.Text);

                    // Checking if Bill is less than or equal to 0, then print that respective error message.
                    if (billAmount <= 0)
                    {
                        printBillRangeMsg();
                    }
                    else
                    {
                        int val = getNoOfPeople();

                        // Retrieving current value of Number of people before decre-menting and if it is 1, then decrementing further
                        // is not possible as minimum no. of people should be at least 1 to give tip to them, and showing
                        // respective error message.
                        if (val == 1)
                        {
                            MessageBox.Show("No. of people cannot be less than 1!");
                            ResetFields();
                        }
                        else
                        {
                            --val;
                            Label2.Text = val.ToString();

                            // Number of people decreased by 1 and calculating new Tip per person and Total per person with new value
                            tipPerPerson(billAmount);
                            totalPerPerson(billAmount);
                        }
                    }
                }
                catch (FormatException exc)
                {
                    // Handling exception in case any invalid amount is entered in Bill box
                    printBillInvalidMsg();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Some error occurred. Try again!");
                    ResetFields();
                }
            }
        }
    }
}
