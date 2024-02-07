using Microsoft.Win32;
using System.Runtime.CompilerServices;



    public static class MyRegistryParamManager
    {
        // Chemin de la clé de registre utilisée pour stocker les paramètres
        private static readonly string RegistryKeyPath = "HEPL\\Labo_final_Bibliotheque";

        // Propriété statique pour la position X
        public static int PositionX
        {
            get => GetValueOrDefault();
            set => SetValue(value);
        }

        // Propriété statique pour la position Y
        public static int PositionY
        {
            get => GetValueOrDefault();
            set => SetValue(value);
        }

        // Propriété statique pour la dimension X
        public static int DimensionX
        {
            get => GetValueOrDefault();
            set => SetValue(value);
        }

        // Propriété statique pour la dimension Y
        public static int DimensionY
        {
            get => GetValueOrDefault();
            set => SetValue(value);
        }

        // Méthode privée pour obtenir la valeur d'une propriété ou une valeur par défaut si elle n'existe pas
        private static int GetValueOrDefault([CallerMemberName] string propertyName = "")
        {
            // Ouverture de la clé de registre
            using (var registryKey = Registry.CurrentUser.OpenSubKey(RegistryKeyPath))
            {
                if (registryKey != null)
                {
                    // Récupération de la valeur correspondant à la propriété
                    var value = registryKey.GetValue(propertyName);

                    // Vérification si la valeur existe et si elle peut être convertie en entier
                    if (value != null && int.TryParse(value.ToString(), out int propertyValue))
                    {
                        return propertyValue;
                    }
                }
            }

            // Valeur par défaut si la clé ou la valeur n'existent pas
            return 0;
        }

        // Méthode privée pour définir la valeur d'une propriété
        private static void SetValue(int value, [CallerMemberName] string propertyName = "")
        {
            // Création de la clé de registre si elle n'existe pas
            using (var registryKey = Registry.CurrentUser.CreateSubKey(RegistryKeyPath))
            {
                if (registryKey != null)
                {
                    // Définition de la valeur de la propriété
                    registryKey.SetValue(propertyName, value);
                }
            }
        }
    }

/*
Ce code définit une classe statique appelée MyRegistryParamManager qui permet de stocker et de récupérer des paramètres dans le registre Windows. 
Les paramètres sont stockés dans une clé spécifique (HEPL\Demo2201). Les paramètres disponibles sont la position X, la position Y, la dimension X et la dimension Y. 
Chaque paramètre est représenté par une propriété statique avec un accesseur en lecture et en écriture. Les valeurs des paramètres sont récupérées à partir du registre et sont stockées sous forme d'entiers. 
Si une valeur par défaut est nécessaire ou si une clé ou une valeur n'existe pas, une valeur par défaut de 0 est renvoyée.
*/