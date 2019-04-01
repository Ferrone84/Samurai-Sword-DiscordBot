using System;
using System.Collections;
using System.Collections.Generic;

namespace KatanaBot {
	public class Choose<T> {
		/* Révéler Tout/Chaque Avant/Après
		pas simple à faire vu que cette instruction brise la mécanique des yields en exigeant de tout connaitre.
		Elle n'a cependant besoin de la briser que dans le GenerateCallArgument, pour présenter les element à l'opérateur et lui laisser choisir.
		Une fois le generate fini, on obtient la liste des elements à conserver et on ne yield que celles dans la liste choisie.
		Dictionnaire index -> choix peut être intéressant */
	}
	public static class Extensions {
	}
}
