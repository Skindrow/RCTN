
using UnityEngine;
[CreateAssetMenu(fileName = "new Resource Adder", menuName = "Loot/ResourcesAdderData")]
public class ResourcesAdder : AdderBehaviour
{
    [SerializeField] private ResourcesStaticDisplay resourcesDisplay;
    [SerializeField] private ResourceData data;
    [SerializeField] private int count;


    public override GameObject InstantiateUIObject(Transform parent)
    {
        ResourcesStaticDisplay resourcesDisplayGO = Instantiate(resourcesDisplay, parent);
        resourcesDisplayGO.SetDisplay(data, count);
        ResourcesManager.Instance.AddResource(data, count);
        return resourcesDisplayGO.gameObject;
    }
}
