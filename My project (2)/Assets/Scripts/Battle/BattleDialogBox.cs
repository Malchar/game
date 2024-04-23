using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] Color highlightedColor;
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] int lettersPerSecond;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject moveDetails;
    [SerializeField] List<TextMeshProUGUI> actionTexts;
    [SerializeField] List<TextMeshProUGUI> moveTexts;
    [SerializeField] TextMeshProUGUI ppText;
    [SerializeField] TextMeshProUGUI typeText;

    public void SetDialog(string dialog){
        dialogText.SetText(dialog);
    }

    public IEnumerator TypeDialog(string dialog){
        dialogText.text = "";
        foreach (var letter in dialog.ToCharArray()){
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/lettersPerSecond);
        }

        yield return new WaitForSeconds(1f);
    }

    public void EnableDialogText(bool enabled){
        dialogText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled){
        actionSelector.SetActive(enabled);
    }

    public void EnableMoveSelector(bool enabled){
        moveSelector.SetActive(enabled);
        moveDetails.SetActive(enabled);
    }

    public void UpdateActionSelection(int selectedAction){
        for (int i=0; i < actionTexts.Count; ++i){
            if (i == selectedAction){
                actionTexts[i].color = highlightedColor;
            } else {
                actionTexts[i].color = Color.black;
            }
        }
    }

    public void SetMoveNames(List<IMove> moves){
        for (int i = 0; i < moveTexts.Count; ++i) {
            if (i < moves.Count){
                moveTexts[i].text = moves[i].Name;
            } else {
                moveTexts[i].text = "";
            }
        }
    }

    public void UpdateMoveSelection(int selectedMove, IMove move){
        for (int i=0; i < moveTexts.Count; ++i){
            if (i == selectedMove){
                moveTexts[i].color = highlightedColor;
            } else {
                moveTexts[i].color = Color.black;
            }
        }

        ppText.text = $"PP {move.Description}";
        typeText.text = "redacted"; //move.Element.ToString();
    }
}
