﻿using MASA.Framework.Admin.Management.Shared;

namespace MASA.Framework.Admin.Management;

public abstract class ProCompontentBase : ComponentBase
{
    private I18n? _languageProvider;

    [CascadingParameter]
    public MainLayout App { get; set; }

    [Inject]
    public I18n LanguageProvider
    {
        get
        {
            return _languageProvider ?? throw new Exception("please Inject I18n!");
        }
        set
        {
            _languageProvider = value;
        }
    }

    public string T(string key)
    {
        return LanguageProvider.T(key) ?? key;
    }

    public void Confirm(
         string title,
         string content,
         Func<Task> onOk,
         AlertTypes type = default,
         string icon = default,
         string iconColor = default,
         string okText = "确定",
         string cancelText = "取消",
         string okColor = "primary",
         string cancelColor = "default",
         Action onCancel = default)
    {
        App.Confirm(title, content, onOk, type, icon, iconColor, okText, cancelText, okColor, cancelColor, onCancel);
    }

    public void Confirm(
        string title,
        string content,
        Action onOk,
        AlertTypes type = default,
        string icon = default,
        string iconColor = default,
        string okText = "确定",
        string cancelText = "取消",
        string okColor = "primary",
        string cancelColor = "default",
        Action onCancel = default)
    {
        Confirm(title, content, () => onOk(), type, icon, iconColor, okText, cancelText, okColor, cancelColor, onCancel);
    }

    public void Message(string content, AlertTypes type = default, int timeout = 3000)
    {
        App.Message(content, type, timeout);
    }
}
