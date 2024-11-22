using System;
using System.Collections.Generic;
using System.Windows.Automation;

namespace marcury_ext.Utils
{
    internal class UtilCheckError
    {
        // Error codes
        public const int NOT_ERROR = 0;
        public const int ERROR_HANDLE_ZERO = 1;
 
        public const int ERROR_UTOMATION_TARGET_NULL = 2;
        public const int ERROR_AUTOMATION_PROPERTY_DISABLED = 3;
        public const int ERROR_HANDLE_TARGET_NOT_EDIT = 4;
        public const int UERROR_UNKNOW_CODE = 100;

        public const string TextBoxForTest = "textBox2";

        /// <summary>
        ///  Dictionary containing error codes and corresponding error messages
        /// </summary>
        public static readonly Dictionary<int, string> ErrorMessages = new Dictionary<int, string>
        {
            { NOT_ERROR, "エラーはありません" }, // NOT_ERROR
            { ERROR_HANDLE_ZERO, "エラー ハンドルが見つかりません" }, // ERROR_HANDLE_ZERO
            { ERROR_HANDLE_TARGET_NOT_EDIT, "制御対象を編集しない" }, // ERROR_HANDLE_TARGET_NOT_EDIT
            { ERROR_UTOMATION_TARGET_NULL, "ERROR: 自動化要素のターゲットが空です" }, // AUTOMATION_TARGET_NULL
            { ERROR_AUTOMATION_PROPERTY_DISABLED, "ERROR: エラー オートメーション要素プロパティが無効です" }, // AUTOMATION_PROPERTY_DISABLED
            { UERROR_UNKNOW_CODE, "ERROR: 未知のエラーコードです。" } // UNKNOW_ERROR_CODE
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
        public static int CheckAutomationElement(AutomationElement autoElement)
        {
            int error = NOT_ERROR;
            // Check autoElement
            if (autoElement is null) error = ERROR_UTOMATION_TARGET_NULL;
            // if # null check isEnabled
            bool isEnabled = (bool)autoElement.GetCurrentPropertyValue(System.Windows.Automation.AutomationElement.IsEnabledProperty);
            if (!isEnabled) error = ERROR_AUTOMATION_PROPERTY_DISABLED;

            if (!IsControlTypeEditOrDocument(autoElement)) error = ERROR_HANDLE_TARGET_NOT_EDIT;
            return error;
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
        public static bool IsControlTypeEditOrDocument(AutomationElement autoElement)
        {
            // Check if ControlType is Edit or Document
            if (autoElement != null) {
               if ((autoElement.Current.ControlType == ControlType.Edit) || (autoElement.Current.ControlType == ControlType.Document)) { return true; }
            }
            return false ;
        }
    }
}
