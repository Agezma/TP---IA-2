using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public Text consoleView;
    public InputField consoleInput;

    public delegate void Commands(params object[] parameterContainer);
    
    Dictionary<string, Commands> consoleLogic = new Dictionary<string, Commands>();
    Dictionary<string, string> commandsExplaind = new Dictionary<string, string>();

    private void Awake()
    {
        AddCommand("Help",Help, "Muestra todos los comandos disponibles");
        AddCommand("Clear", Clear,"Limpia la pantalla de la consola");
        AddCommand("Money", AddMoney, "Agrega x cantidad de dinero");
        AddCommand("KillAll", KillAll, "Mata todos los enemigos actuales");
        AddCommand("Kill", Kill, "Mata x enemigos");
        AddCommand("Level", ChangeScene, "Ir al nivel x");
        AddCommand("Heal", Heal , "Restaura x vida a tu base");
        AddCommand("InstaLose", Lose,"Destruye tu base");
    }

    void AddCommand(string key, Commands myEvent, string explaind)
    {
        key = key.ToLower();
        if (!consoleLogic.ContainsKey(key))
            consoleLogic.Add(key, myEvent);
        else
            consoleLogic[key] += myEvent;

        if (!commandsExplaind.ContainsKey(key))
            commandsExplaind.Add(key, explaind);
    }

    void CheckKey(string k)
    {
        char[] delimiter = new char[] { '-', ' ' };
        string[] substrings = k.Split(delimiter);
        int value = -1;

        if (substrings.Length > 1)
            value = int.Parse(substrings[substrings.Length - 1]);
           

        if (consoleLogic.ContainsKey(substrings[0]))
        {
            if (value != -1)
                consoleLogic[substrings[0]](new object[] { value });
            else
                consoleLogic[substrings[0]]();
        }
        else
            consoleView.text += "Comando Incorrecto." + "\n";
    }

    #region Actions

    void Clear(params object[] parameters)
    {
        consoleView.text = "";
    }

    void Help(params object[] parameters)
    {
        Clear();
        foreach (var c in consoleLogic)
        {
            consoleView.text += "-"+c.Key+": "+commandsExplaind[c.Key]+ "\n";
        }
    }

    void AddMoney(params object[] paramters)
    {
        if((int)paramters[0] < 0)
        {
            consoleView.text += "No se puede agregar esa cantidad \n";
        }
        Main.Instance.myMoneyManager.money += (int)paramters[0];
        consoleView.text += "Dinero agregado: " + (int)paramters[0] + "\n";
    }

    void AddDamage(params object[] paramters)
    {
        Main.Instance.player.charAttack.damage += (int)paramters[0];
    }

    void KillAll(params object[] paramters)
    {
        var enemies = Main.Instance.enemyManager.enemiesAlive;
        consoleView.text += enemies.Count + " enemigos eliminados \n";
        while(enemies.Count > 0)
        {
            enemies[0].TakeDamage(1000);
        }
    }

    void Kill(params object[] paramters)
    {
        var enemies = Main.Instance.enemyManager.enemiesAlive;
        
        int aux = (int)paramters[0];
        if (aux > enemies.Count) aux = enemies.Count;

        consoleView.text += aux + " enemigos eliminados \n";

        for (int i = 0; i < aux; i++)
        {
            enemies[0].TakeDamage(1000);
        }
    }

    void ChangeScene(params object[] paramters)
    {
        if( 0 < (int)paramters[0] && (int)paramters[0] <= 3)
        {
            Main.Instance.sceneManager.LoadScene((int)(paramters[0]));
            consoleView.text += "Cargando nivel " + (int)paramters[0] + "\n";
        }
        else
        {
            consoleView.text += "Seleccione un nivel del 1 al 3 \n";
        }
    }

    void Lose(params object[] paramters)
    {
        Main.Instance.baseToAttack.TakeDamage(100);
    }

    void Heal(params object[] paramters)
    {
        int life = (int)paramters[0];
        int max = (int)Main.Instance.baseToAttack.startingLife;
        if (life > 0)
        {
            Main.Instance.baseToAttack.TakeDamage(-life);
            if (Main.Instance.baseToAttack.lives > max)
            {
                Main.Instance.baseToAttack.lives = max;
            }
            consoleView.text += "Vida de la base: " + Main.Instance.baseToAttack.lives + "\n";
        }

    }

    #endregion

    public void EnterNewCommmand()
    {
        CheckKey(consoleInput.text.ToLower());
    }

}
