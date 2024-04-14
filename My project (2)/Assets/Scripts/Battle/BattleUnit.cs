using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] PartyMember partyMember;
    [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;

    Image image;
    Vector3 originalPos;
    Color originalColor;

    private void Awake(){
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
        originalColor = image.color;
    }

    public PartyMember PartyMember { get => partyMember; set => partyMember = value; }

    public void Setup(PartyMember inPartyMember){
        partyMember = inPartyMember;
        if (isPlayerUnit) {
            image.sprite = PartyMember.JobBase.BackSprite;
        }
        else {
            image.sprite = PartyMember.JobBase.FrontSprite;
        }

        PlayEnterAnimation();
    }

    public void PlayEnterAnimation(){
        if (isPlayerUnit){
            image.transform.localPosition = new Vector3(-500f, originalPos.y);
        } else {
            image.transform.localPosition = new Vector3(500f, originalPos.y);
        }

        image.transform.DOLocalMoveX(originalPos.x, 1f);
    }

    public void PlayAttackAnimation(){
        var sequence = DOTween.Sequence();
        if (isPlayerUnit){
            sequence.Append(image.transform.DOLocalMoveX(originalPos.x + 50f, 0.25f));
        } else {
            sequence.Append(image.transform.DOLocalMoveX(originalPos.x - 50f, 0.25f));
        }

        sequence.Append(image.transform.DOLocalMoveX(originalPos.x, 0.25f));
    }

    public void PlayHitAnimation(){
        var sequence = DOTween.Sequence();
        sequence.Append(image.DOColor(Color.gray, 0.1f));
        sequence.Append(image.DOColor(originalColor, 0.1f));
    }

    public void PlayFaintAnimation(){
        var sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOLocalMoveY(originalPos.y - 150f, 0.5f));
        sequence.Join(image.DOFade(0f, 0.5f));
    }
}