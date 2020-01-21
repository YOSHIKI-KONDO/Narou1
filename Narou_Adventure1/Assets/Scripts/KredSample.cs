using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class KredSample : BASE
{
    private void Awake()
    {
        StartBASE();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Purchase);
        gameObject.name = "KredsSample";
        ItemRequest();
    }

    //purchase10ruby()で呼ばれる
    public void OnPurchaseResult(string success)
    {
        if(success == "true")
        {
            main.announce.Add("OnPurchaseResult : success");
            ItemRequest();
            
        }
        else
        {
            main.announce.Add("OnPurchaseResult : failed");
        }
    }

    public void Purchase()
    {
        Application.ExternalEval(@"
             kongregate.mtx.purchaseItems(['sample1'], function(result) {
             var unityObject = kongregateUnitySupport.getUnityObject();
             var success = String(result.success);
             unityObject.SendMessage('KredsSample','OnPurchaseResult', success);
            });
        ");

    }

    public void ItemRequest()
    {
        Application.ExternalEval(@"
        kongregate.mtx.requestUserItemList(null , function(result) {
            console.log('User item list received, success: ' + result.success);
            var unityObject = kongregateUnitySupport.getUnityObject();
                if (result.success)
                    {
              for (var i = 0; i < result.data.length; i++)
              {
                var item = result.data[i];
                console.log((i + 1) + '. ' + item.identifier + ', ' +
                            item.id + ',' + item.data);
                if(item.identifier == 'sample1'){
                kongregate.mtx.useItemInstance(item.id, onUseResult);
                break;
                        }
                   }
                }
                
          })

        function onUseResult(result) {
                console.log('Item use result successful: ' + result.success);
                var unityObject = kongregateUnitySupport.getUnityObject();
                   var success = String(result.success);
                 unityObject.SendMessage('KredsSample','UseItem',success);
}

        ");                   
    }

    //上のonUseResultで呼ばれる
    public void UseItem(string success)
    {
        if(success == "true")
        {
            main.announce.Add("Success.");
        }
        else if(success == "false")
        {
            main.announce.Add("failed.");
        }
    }

}
