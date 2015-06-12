var Estado : int = 0;

var Target: GameObject; //El objetivo

private var RotInic : Quaternion;
var RotSpeed : float;

var VelMov : float;
var gravity : float = 9.8;

private var DistanciaCont : float;
var DistEnem : float = 0.5;

private var contador = 0.0;

//Animaciones

var IdleAnim : AnimationClip;
var RunAnim : AnimationClip;
var GuardAnim : AnimationClip;
var AttackAnim : AnimationClip;

var MuerteAnim : AnimationClip;

function Start ()
{
   //wrapMode puede ser: Once, Loop, PingPong, Default o ClampForever
   animation[IdleAnim.name].speed = 1;
   animation[IdleAnim.name].wrapMode = WrapMode.Loop;

   animation[RunAnim.name].speed = 1;
   animation[RunAnim.name].wrapMode = WrapMode.Loop;

   animation[GuardAnim.name].speed = 1;
   animation[GuardAnim.name].wrapMode = WrapMode.Loop;

   animation[AttackAnim.name].speed = 1;
   animation[AttackAnim.name].wrapMode = WrapMode.Once;

   animation[MuerteAnim.name].speed = 1;
   animation[MuerteAnim.name].wrapMode = WrapMode.ClampForever;

   RotInic = transform.rotation;

   animation.Play(IdleAnim.name);
   
   if (Target == null)
   {
      Target = GameObject.FindGameObjectWithTag("Player");
   }

}

function Update () 
{

   var controller : CharacterController = GetComponent(CharacterController);

   if (Estado == 0)
   {
      //Acción	
      //Cambio de Estado y Activar siguiente animación.
      //Se cambia mediante un disparador externo.
   }
   if (Estado == 1) //Perseguir
   {
      //Acción
      //Girar y avanzar.
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Target.transform.position - transform.position), RotSpeed * Time.deltaTime);
      transform.rotation.x = RotInic.x;
      transform.rotation.z = RotInic.z;			
      //Cambio de Estado y Activar siguiente animación.
	  
      controller.Move(transform.forward * VelMov * Time.deltaTime);
      //Aplicar gravedad
      controller.Move(transform.up * -gravity * Time.deltaTime);
	  
      //Cambio de Estado y Activar siguiente animación.

      DistanciaCont = Vector3.Distance(Target.transform.position, transform.position);
      if (DistanciaCont <= DistEnem)
      {
         Estado = 2;
         animation.CrossFade(GuardAnim.name);
      }
   }
   if (Estado == 2) //Guardia
   {
      //Acción
      //Cambio de Estado y Activar siguiente animación.
      DistanciaCont = Vector3.Distance(Target.transform.position, transform.position);
      if (DistanciaCont > DistEnem)
      {
         Estado = 1; //Pasa al estado de perseguir.
         animation.CrossFade(RunAnim.name);
      }else{
         Estado = 3;//Pasa al estado de Atacar.
         contador = Time.time + (animation[AttackAnim.name].clip.length * 1.2);
         //El tiempo actual + (el tiempo de la animación y un pelín más)
         animation.Play(AttackAnim.name);		 
      }
   }
   if (Estado == 3) // Atacar
   {
      //Acción
   
      //Cambio de Estado y Activar siguiente animación.
      if (Time.time > contador)
      {
         Estado = 2;
         animation.CrossFade(GuardAnim.name, 2.0f);
      }
   }
   
   
   
}

function Muerte ()
{
   Estado =9;
   animation.Play (MuerteAnim.name);
}

function DoActivateTrigger ()
{
   Estado = 1;
   animation.CrossFade(RunAnim.name);
}

function DoDesactivateTrigger ()
{
   Estado = 0; //cambiar a al estado de Inactivo
   animation.Play(IdleAnim.name);
}