using MIIOT.DiagManager.Model.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MIIOT.UI.Controls
{
    public class NbFormPage : NbPage
    {
        /// <summary>
        /// 自动验证表单参数前执行,；若返回验证失败，则不再执行自动验证表单参数
        /// </summary>
        public FormValidateHandle BeforeFormValidateHandle { get; set; }
        /// <summary>
        /// 自动验证表单参数后执行；若自动验证表单失败，则不执行该验证。
        /// </summary>
        public FormValidateHandle AfterFormValidateHandle { get; set; }

        /// <summary>
        /// 验证DataContext绑定对象的属性
        /// </summary>
        public virtual async Task<ModelValidationResult> ValidateAsync()
        {
            //var rets = new List<ModelValidationResult>();

            var data = this.DataContext;
            if (BeforeFormValidateHandle != null)
            {
                ModelValidationResult beforeRet = null;
                await Task.Run(() =>
                {
                    try
                    {
                        beforeRet = BeforeFormValidateHandle.Invoke(data) ?? ModelValidationResult.Ok();
                    }
                    catch
                    {
                        beforeRet = ModelValidationResult.Error("系统异常");
                    }
                });
                if (!beforeRet.CheckAccess)
                {
                    return beforeRet;
                }
            }
            

            var autoRets = ModelValidation.Validate(this.DataContext);
            if (autoRets.Count > 0)
            {
                return ModelValidationResult.Error(string.Join(Environment.NewLine, autoRets.Select(p => p.Message)));
            }


            if(AfterFormValidateHandle != null)
            {
                ModelValidationResult afterRet = null;

                await Task.Run(() =>
                {
                    try
                    {
                        afterRet = AfterFormValidateHandle.Invoke(data) ?? ModelValidationResult.Ok();
                    }
                    catch
                    {
                        afterRet = ModelValidationResult.Error("系统异常");
                    }
                });
                
                if (!afterRet.CheckAccess)
                {
                    return afterRet;
                }
            }
            

            return ModelValidationResult.Ok();
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataContext"></param>
    /// <returns></returns>
    public delegate ModelValidationResult FormValidateHandle(object dataContext);

    //public class FormValidateResult
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public bool CheckAccess { get; set; }
    //    /// <summary>
    //    /// 是否执行自动验证属性
    //    /// BeforeFormValidateHandle中，如果赋值false，不再执行自动验证属性
    //    /// </summary>
    //    public bool Handle { get; set; } = true;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string Message { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="handle"></param>
    //    /// <returns></returns>
    //    public static FormValidateResult Ok(bool handle = true)
    //    {
    //        return new FormValidateResult() { CheckAccess = true,Handle = handle };
    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="message"></param>
    //    /// <param name="handle"></param>
    //    /// <returns></returns>
    //    public static FormValidateResult Error(string message, bool handle = true)
    //    {
    //        return new FormValidateResult() { CheckAccess = false, Handle = handle, Message = message };
    //    }
    //}
}
