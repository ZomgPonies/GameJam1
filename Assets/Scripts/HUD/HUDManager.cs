using System;
using UnityEngine;

public class HUDManager : MonoSingleton<HUDManager>
{
    // Available HUDs
    public CanvasGroup loadingCanvas;

    public CanvasGroup CurrentHudCanvasGroup { get; private set; }

    private float fadeDuration;

    private bool isInTransition;
    private bool isShowing;
    private float currentTransitionAlpha;
    private float currentTransitionDuration;
    private Action callBackMethod;

    private HUDManager()
    {
        fadeDuration = 0.8f;
    }

    public void HideAllHUDs()
    {
        ChangeCanvasGroupAlpha(loadingCanvas, 0.0f);
    }

    public void ChangeDisplayedHUD(CanvasGroup canvasGroup)
    {
        if (CurrentHudCanvasGroup != null) ChangeCanvasGroupAlpha(CurrentHudCanvasGroup, 0.0f);
        ChangeCurrentHUD(canvasGroup);
    }

    private void ChangeCurrentHUD(CanvasGroup canvasGroup)
    {
        CurrentHudCanvasGroup = canvasGroup;
        ChangeCanvasGroupAlpha(canvasGroup, 1.0f);
    }

    private void ChangeCanvasGroupAlpha(CanvasGroup canvasGroup, float alpha)
    {
        canvasGroup.alpha = alpha;
    }

    public void FadeOutHud(CanvasGroup canvasGroup, Action callBackMethod = null)
    {
        if(CurrentHudCanvasGroup != null) Fade(false, fadeDuration);
        this.callBackMethod = callBackMethod;
    }

    public void FadeInHud(CanvasGroup canvasGroup, Action callBackMethod = null)
    {
        CurrentHudCanvasGroup = canvasGroup;
        Fade(true, fadeDuration);
        this.callBackMethod = callBackMethod;
    }

    private void Fade(bool showing, float duration)
    {
        isShowing = showing;
        isInTransition = true;
        currentTransitionDuration = duration;
        currentTransitionAlpha = (isShowing) ? 0 : 1;
    }

    private void Update()
    {
        if (!isInTransition) return;

        currentTransitionAlpha += (isShowing) ? Time.deltaTime * (1 / currentTransitionDuration) : -Time.deltaTime * (1 / currentTransitionDuration);

        float alpha1 = CurrentHudCanvasGroup.alpha;
        float alpha2 = CurrentHudCanvasGroup.alpha;
        alpha1 = 0;
        alpha2 = 1;

        CurrentHudCanvasGroup.alpha = Mathf.Lerp(alpha1, alpha2, currentTransitionAlpha);

        if (currentTransitionAlpha >= 1 || currentTransitionAlpha <= 0)
        {
            isInTransition = false;

            if (callBackMethod != null) callBackMethod();
        }
    }
}
