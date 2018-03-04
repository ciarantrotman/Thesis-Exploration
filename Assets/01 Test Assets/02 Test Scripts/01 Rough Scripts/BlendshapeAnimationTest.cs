using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendshapeAnimationTest : MonoBehaviour {

    SkinnedMeshRenderer skinnedMeshRenderer;

    float HoverBlend = 0.0f;
    float ClickBlend = 0.0f;

    [Header("Blendshape Order Values")]
    public int Hover = 1;
    public int Click = 0;
    [Space(10)]

    float UpperTarget = 100.0F;
    float LowerTarget = 0.0F;

    [Header("Hover Animation Settings")]
    public float HoverBlendStartDuration = 1.0F;
    public float HoverBlendEndDuration = 1.0F;

    [Header("Click Animation Settings")]
    public float ClickBlendStartDuration = 1.0F;
    public float ClickBlendEndDuration = 1.0F;

    private float BlendVelocity = 0.0f;
    private bool IsHover = false;
    private bool IsClick = false;

    void Awake()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    public void OnHoverStart()
    {
        IsHover = true;
        StartCoroutine(HoverStart());
    }

    public IEnumerator HoverStart()
    {
        while (IsHover == true && HoverBlend < 99.0f)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(Hover, HoverBlend);
            HoverBlend = (Mathf.SmoothDamp(HoverBlend, UpperTarget, ref BlendVelocity, HoverBlendStartDuration));

            yield return null;
        }
        
    }

    public void OnHoverEnd()
    {
        IsHover = false;
        StartCoroutine(HoverEnd());
    }

    public IEnumerator HoverEnd()
    {
        while (HoverBlend > 1.0f)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(Hover, HoverBlend);
            HoverBlend = (Mathf.SmoothDamp(HoverBlend, LowerTarget, ref BlendVelocity, HoverBlendEndDuration));

            yield return null;

        }
    }

    public void OnPress()
    {
        IsClick = true;
        StartCoroutine(PressStart());
    }

    public IEnumerator PressStart()
    {
        while (IsClick == true && ClickBlend < 100.0f)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(Click, ClickBlend);
            ClickBlend = (Mathf.SmoothDamp(ClickBlend, UpperTarget, ref BlendVelocity, ClickBlendStartDuration));

            yield return null;
        }
    }

    public void OnUnpress()
    {
        IsClick = false;
        StartCoroutine(UnpressStart());
    }

    public IEnumerator UnpressStart()
    {
        while (ClickBlend > 1.0f)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(Click, ClickBlend);
            ClickBlend = (Mathf.SmoothDamp(ClickBlend, LowerTarget, ref BlendVelocity, ClickBlendEndDuration));

            yield return null;
        }
    }
}

/*
void Update()
{
    if (Input.GetKeyDown(KeyCode.UpArrow))
    {
        while (HoverBlend < UpperTarget)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(0, HoverBlend);
            HoverBlend = (Mathf.SmoothDamp(HoverBlend, StartingValue, ref UpperTarget, BlendDuration));
        }

    }

    if (Input.GetKeyDown(KeyCode.DownArrow))
    {
        skinnedMeshRenderer.SetBlendShapeWeight(1, ClickBlend);
    }
}
*/

/*
 private IEnumerator ShowHUD() {
    var HUDCanvasGroup = GameObject.FindWithTag("HUD").GetComponent<CanvasGroup>();
    var alphaValue = 1f;
    var velocity = 0f;
    var time = 0.30f;

    yield return new WaitForSeconds(1.5f);

    // Gradually fade in the canvas
    while (!Mathf.Approximately(HUDCanvasGroup.alpha, alphaValue)) {
        HUDCanvasGroup.alpha = Mathf.SmoothDamp(HUDCanvasGroup.alpha, alphaValue, ref velocity, time);
        yield return null;
    }

    HUDCanvasGroup.alpha = 1; // Since float is not accurate, manually set the alpha to 1 after
    HUDCanvasGroup.interactable = true;

    var blur = GameObject.FindWithTag("BlurryCamera").GetComponent<BlurOptimized>();
    blur.enabled = true;
}
 */

/*
public Transform target;
public float smoothTime = 0.3F;
private Vector3 velocity = Vector3.zero;
void Update()
{
    Vector3 targetPosition = target.TransformPoint(new Vector3(0, 5, -10));
    transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
}
}
*/
