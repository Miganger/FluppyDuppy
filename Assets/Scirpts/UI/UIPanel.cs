using UnityEngine;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public virtual void Open()
    {
        EnablePanel();

        if (!animator)
            return;

        SetTrigger("In");
    }
    public virtual void Close()
    {
        if (animator)
        {
            SetTrigger("Out");
            return;
        }
        DisablePanel();
    }

    private void SetTrigger(string name)
    {
        ClearTriggers();
        animator.SetTrigger(name);
    }
    private void ClearTriggers()
    {
        animator.ResetTrigger("In");
        animator.ResetTrigger("Out");
    }

    public void EnablePanel()
    {
        gameObject.SetActive(true);
    }
    public void DisablePanel()
    {
        gameObject.SetActive(false);
    }
}