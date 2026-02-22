using System;

namespace BMI_Calculator;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        heightUnitPicker.Items.Add("Centimeters (cm)");
        heightUnitPicker.Items.Add("Feet/Inches");
        heightUnitPicker.SelectedIndex = 0;

        weightUnitPicker.Items.Add("Kilograms (kg)");
        weightUnitPicker.Items.Add("Pounds (lbs)");
        weightUnitPicker.SelectedIndex = 0;
    }

    private void OnHeightUnitChanged(object sender, EventArgs e)
    {
        bool isCm = heightUnitPicker.SelectedIndex == 0;

        heightCmEntry.IsVisible = isCm;
        feetInchesLayout.IsVisible = !isCm;
    }

    private void OnCalculateClicked(object sender, EventArgs e)
    {
        try
        {
            double heightMeters = 0;
            double weightKg = 0;

            // Height
            if (heightUnitPicker.SelectedIndex == 0)
            {
                double heightCm = double.Parse(heightCmEntry.Text);
                heightMeters = heightCm / 100;
            }
            else
            {
                double feet = double.Parse(feetEntry.Text);
                double inches = double.Parse(inchesEntry.Text);

                double totalInches = (feet * 12) + inches;
                heightMeters = (totalInches * 2.54) / 100;
            }

            // Weight
            if (weightUnitPicker.SelectedIndex == 0)
                weightKg = double.Parse(weightEntry.Text);
            else
                weightKg = double.Parse(weightEntry.Text) * 0.453592;

            double bmi = weightKg / (heightMeters * heightMeters);
            string category = GetBMICategory(bmi);

            bmiValueLabel.Text = $"BMI: {bmi:F2}";
            bmiCategoryLabel.Text = category;

            resultFrame.IsVisible = true;
            resultFrame.BackgroundColor = GetCategoryColor(bmi);
        }
        catch
        {
            resultFrame.IsVisible = true;
            bmiValueLabel.Text = "Invalid Input";
            bmiCategoryLabel.Text = "Please enter valid numbers";
            resultFrame.BackgroundColor = Colors.LightGray;
        }
    }

    private string GetBMICategory(double bmi)
    {
        if (bmi < 18.5)
            return "Underweight";
        else if (bmi < 24.9)
            return "Normal Weight";
        else if (bmi < 29.9)
            return "Overweight";
        else
            return "Obese";
    }

    private Color GetCategoryColor(double bmi)
    {
        if (bmi < 18.5)
            return Colors.LightBlue;
        else if (bmi < 24.9)
            return Colors.LightGreen;
        else if (bmi < 29.9)
            return Colors.Orange;
        else
            return Colors.Red;
    }
}