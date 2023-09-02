using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NpcDialogSystem : MonoBehaviour
{
    public List<string> _dialogList = new List<string>();

    [SerializeField] private DialogHolder _dialog;
    [SerializeField] private GameObject _textBox;
    [SerializeField] private TMP_Text _text;

    public Transform Npc;
    public Transform deneme;


    private bool isOpenDialog;
    private int _textCount;

    void Start()
    {
        _dialogList = _dialog.DialogTexts.ToList();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            LookDirection(deneme);

        if (isOpenDialog)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                NextText();
            }
        }
        
    }

    private void OpenDialogBox()
    {
        isOpenDialog = true;
        _textBox.SetActive(true);
        _text.text = _dialogList[_textCount];
        _textCount++;
    }

    private void NextText()
    {
        if (_textCount >= _dialogList.Count)
        {
            CloseMenu();
        }
        else
        {
            _text.text = _dialogList[_textCount];
            _textCount++;
        }
         
    }

    private void CloseMenu()
    {
        _textCount = 0;
        _textBox.SetActive(false);

        var targetRotation = Quaternion.LookRotation(Vector3.zero, Vector3.up);
        Npc.DORotateQuaternion(targetRotation, .3f).SetEase(Ease.OutSine);
    }
    public void LookDirection(Transform playerTransform)
    {
        var vector = new Vector3(playerTransform.position.x, Npc.position.y, playerTransform.position.z);
        var dir = (Npc.position - vector).normalized;

        var targetRotation = Quaternion.LookRotation(dir, Vector3.up);

        Npc.DORotateQuaternion(targetRotation, .3f).SetEase(Ease.OutSine).OnComplete(()=> { OpenDialogBox(); });
    }
}
