using System;
using System.Collections.Generic;
using System.Windows.Automation;

namespace marcury_ext.ThrowException
{
    internal class UtilErrors
    {
        // Error codes
        public const int SUCCESS = 0;
        public const int ERROR_HANDLE_ZERO = 1;

        public const int ERROR_AUTOMATION_TARGET_NULL = 2;
        public const int ERROR_AUTOMATION_PROPERTY_DISABLED = 3;
        public const int ERROR_HANDLE_TARGET_NOT_EDIT = 4;
        public const int ERROR_UNKNOW_CODE = 100;

        public const string TextBoxForTest = "textBox2";
        //Declare readonly list of ControlTypes
        private static readonly List<ControlType> controlTypeCanNotEditList = new List<ControlType>
        {
            ControlType.Window,
            ControlType.Button,
            ControlType.CheckBox,
            ControlType.RadioButton,
            ControlType.ComboBox,
            ControlType.List,
            ControlType.Image,
            ControlType.Slider,
            ControlType.Tab
        };

        /// <summary>
        ///  Dictionary containing error codes and corresponding error messages
        /// </summary>
        public static readonly Dictionary<int, string> ErrorMessages = new Dictionary<int, string>
        {
            { SUCCESS, "エラーはありません" }, // NOT_ERROR
            { ERROR_HANDLE_ZERO, "エラー ハンドルが見つかりません" }, // ERROR_HANDLE_ZERO
            { ERROR_HANDLE_TARGET_NOT_EDIT, "制御対象を編集しない" }, // ERROR_HANDLE_TARGET_NOT_EDIT
            { ERROR_AUTOMATION_TARGET_NULL, "ERROR: 自動化要素のターゲットが空です" }, // AUTOMATION_TARGET_NULL
            { ERROR_AUTOMATION_PROPERTY_DISABLED, "ERROR: エラー オートメーション要素プロパティが無効です" }, // AUTOMATION_PROPERTY_DISABLED
            { ERROR_UNKNOW_CODE, "ERROR: 未知のエラーコードです。" } // UNKNOW_ERROR_CODE
        };

        /// <summary>
        /// GetErrorMessage
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static string GetErrorMessage(int errorCode)
        {
            if (ErrorMessages.TryGetValue(errorCode, out string errorMessage)) {
                return errorMessage;
            }
            return "システムエラー。"; // システムエラー
        }


        /// <summary>
        /// CheckAutomationElement
        /// </summary>
        /// <param name="autoElement"></param>
        /// <returns></returns>
        public static void CheckAutomationElement(AutomationElement autoElement)
        {
            // Check autoElement
            if (autoElement is null) throw new MarcuryExtractException(ERROR_AUTOMATION_TARGET_NULL, GetErrorMessage(ERROR_HANDLE_ZERO));
            // if # null check isEnabled
            bool isEnabled = (bool)autoElement.GetCurrentPropertyValue(System.Windows.Automation.AutomationElement.IsEnabledProperty);
            if (!isEnabled) throw new MarcuryExtractException(ERROR_AUTOMATION_PROPERTY_DISABLED, GetErrorMessage(ERROR_AUTOMATION_PROPERTY_DISABLED));

            // Check case ControlType not edit text
            if (IsControlTypeCanNotEditText(autoElement)) throw new MarcuryExtractException(ERROR_HANDLE_TARGET_NOT_EDIT, GetErrorMessage(ERROR_HANDLE_TARGET_NOT_EDIT));
        }

        /// <summary>
        /// Check support UI Automation
        /// </summary>
        /// <param name="handleTarget"></param>
        /// <returns></returns>
        public static bool CheckIfTextBoxSupportsUIAutomation(IntPtr handleTarget)
        { 
            try {
                // Get AutomationElement from TextBox handle
                AutomationElement autoElement = AutomationElement.FromHandle(handleTarget);

                // Check if the control supports UI Automation by checking its properties.
                if (autoElement != null) {
                    // Check if the IsEnabled property is accessible (controls that support UI Automation)
                    return (bool)autoElement.GetCurrentPropertyValue(AutomationElement.IsEnabledProperty);
                }
            } catch (Exception ex) {
                // If you get an error, it may be that the control does not support UI Automation.
                Console.WriteLine("Error: " + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Check if ControlType is Edit or Document
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static bool IsControlTypeCanNotEditText(AutomationElement autoElement)
        {
            // Check if ControlType is Edit or Document
            return controlTypeCanNotEditList.Contains(autoElement.Current.ControlType);
        }
    }
}
