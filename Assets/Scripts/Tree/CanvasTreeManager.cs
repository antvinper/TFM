using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CanvasTreeManager : MonoBehaviour
{
    private List<TreeBranchView> treeBranches;
    [SerializeField] private Transform panelTree;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private TextMeshProUGUI soulFragmentText;
    private void Start()
    {
        treeBranches = new List<TreeBranchView>();
        /*treeBranches = new List<TreeBranchView>(GetComponentsInChildren<TreeBranchView>());

        foreach(TreeBranchView branch in treeBranches)
        {
            branch.FillBranchView();
        }*/
        RefreshSoulFragmentText();
    }

    private async Task RefreshSoulFragmentText()
    {
        PlayerController playerController = GameManager.Instance.GetPlayerController();
        while(playerController == null)
        {
            playerController = GameManager.Instance.GetPlayerController();
            await new WaitForSeconds(0.1f);
        }
        
        //await new WaitForSeconds(1.0f);
        //PlayerController playerController = GameManager.Instance.GetPlayerController();

        soulFragmentText.text = playerController.SoulFragments.Amount + "$";
    }

    public void Setup(List<TreeSlot> treeSlots)
    {
        foreach(TreeSlot slot in treeSlots)
        {
            TreeBranchView branch = Instantiate(slotPrefab).GetComponent<TreeBranchView>();
            branch.transform.SetParent(panelTree);
            treeBranches.Add(branch);
            branch.Setup(slot);

            branch.IncrementTreeBranchButton.onClick.AddListener(() => IncrementSlotLevel(slot, branch));
        }
    }

    public void IncrementSlotLevel(TreeSlot slot, TreeBranchView branch)
    {
        int price = slot.GetActualPrice();
        PlayerController playerController = GameManager.Instance.GetPlayerController();


        if (price < playerController.SoulFragments.Amount)
        {
            GameManager.Instance.GetPlayerController().ActiveSlotTree(slot);
            branch.RefreshUI(slot);

            playerController.AddSoulFragments(-price);

            RefreshSoulFragmentText();
        }
        
    }
}
