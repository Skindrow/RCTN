
using UnityEngine;
[CreateAssetMenu(fileName = "new Resource Adder", menuName = "Loot/ResourcesAdderData")]
public class ResourcesAdder : AdderBehaviour
{
    [SerializeField] private ResourcesDisplay resourcesDisplay;
    [SerializeField] private Resource.ResourcesType resourceType;
    [SerializeField] private int count;


    public override GameObject InstantiateUIObject(Transform parent)
    {
        ResourcesDisplay resourcesDisplayGO = Instantiate(resourcesDisplay, parent);
        resourcesDisplayGO.UpdateDisplay(resourceType, count);
        //ResourcesManager.Instance.AddResource()
        return null;
    }
}
