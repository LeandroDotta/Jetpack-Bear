using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour {

	public UICoinPanel coinPanel;
	public Button backButton;
	public GameObject panelNoCoins;

	[Header("Containers")]
	public Transform storeItemContainer;
	public Transform powerUpsContainer;

	[Header("Prefabs")]
	public GameObject prefabStoreItem;
	public GameObject prefabPowerUpItem;

	public PowerUpInfo[] powerUps;

	private Dictionary<PowerUpId, PowerUp> powerUpDict;

	void Awake()
	{
		powerUpDict = new Dictionary<PowerUpId, PowerUp>();

		foreach(PowerUpInfo info in powerUps)
		{
			PowerUp powerUp = DataManager.GetPowerUpData(info.id);
			powerUp.Info = info;
			powerUpDict.Add(powerUp.id, powerUp);

			// Adiciona o botão para comprar o powerup
			UIStoreItem storeItem = Instantiate(prefabStoreItem).GetComponent<UIStoreItem>();
			storeItem.transform.name = "StoreItem_" + info.id.ToString();
			storeItem.transform.SetParent(storeItemContainer);
			Navigation nav = storeItem.GetComponent<Button>().navigation;
			nav.mode = Navigation.Mode.None;
			storeItem.GetComponent<Button>().navigation = nav;
			storeItem.gameObject.SetActive(true);
			storeItem.Info = info;
			storeItem.UpdateState(powerUp);
			storeItem.buttonBuy.onClick.AddListener(() => BuyPowerUp(info));

			// Adiciona o botão na lista de power ups adquiridos, caso haja algum
			if(powerUp.units > 0)
			{
				prefabPowerUpItem.GetComponent<UIPowerUpItem>().info = info;
				UIPowerUpItem powerUpItem = Instantiate(prefabPowerUpItem).GetComponent<UIPowerUpItem>();
				powerUpItem.transform.name = "PowerUpItem_" + info.id.ToString();
				powerUpItem.transform.SetParent(powerUpsContainer);
				powerUpItem.gameObject.SetActive(true);
				powerUpItem.SetUnitsText(powerUp.units);
			}
		}
	}

	void Start()
	{
		backButton.Select();
	}

	// Atualiza a lista de powerups adquiridos pelo jogador.
	public void UpdatePowerUp(PowerUp powerUp)
	{
		Transform x = powerUpsContainer.Find("PowerUpItem_" + powerUp.id.ToString());

		// Caso o powerup já esteja na lista, a quandidade é incrementada com uma animação.
		if(x != null)
		{
			UIPowerUpItem item = x.GetComponent<UIPowerUpItem>();
			item.UpdateUnits(powerUp.units);
			return;
		}

		// Caso o powerup não estaja na lista de powerups adquiridos, ele é inserido na tela com uma animação
		prefabPowerUpItem.GetComponent<UIPowerUpItem>().info = powerUp.Info;
		UIPowerUpItem powerUpItem = Instantiate(prefabPowerUpItem).GetComponent<UIPowerUpItem>();
		powerUpItem.transform.name = "PowerUpItem_" + powerUp.Info.id.ToString();
		powerUpItem.transform.SetParent(powerUpsContainer);
		powerUpItem.gameObject.SetActive(true);
		powerUpItem.SetUnitsText(powerUp.units);
	}

	public void Close()
	{
		SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
	}

	public void BuyPowerUp(PowerUpInfo info)
	{
		PowerUp powerUp = powerUpDict[info.id];

		// Verificar se tem moedas suficiente e exibir mensagem caso não possua
		if(DataManager.Coins >= powerUp.Info.price)
		{
			// Toca som
			SoundEffects.Instance.Play(SoundEffects.Instance.sfxCash);

			// Remover valor do preço das moedas
			coinPanel.RemoveCoins(powerUp.Info.price);
			DataManager.Coins -= powerUp.Info.price;

			// Ativar power up (se for escudo, o ativa. Se for ímã, adiciona mais itens do mesmo)
			if(powerUp.Info.isAccumulative)
				powerUp.units += powerUp.Info.units;
			else
				powerUp.units = 1;

			DataManager.SetPowerUpData(powerUp);
			
			// Atualizar o botão da lista
			UIStoreItem clickedItem = EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetComponent<UIStoreItem>();
			clickedItem.UpdateState(powerUp);

			// Atualiza os powerups ativos
			UpdatePowerUp(powerUp);
		}else
		{
			// Exibir janela informando que não há moedas suficiente
			panelNoCoins.SetActive(true);
		}
		
	}

	// TODO: Implementar método: Adiquirir moedas ao ver anuncio
	public void AddCoin()
	{
		throw new NotImplementedException();
	}
}
