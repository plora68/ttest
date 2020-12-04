using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Drawing;

namespace test
{
    // http://www.csharpstudy.com/CSharp/CSharp-yield.aspx
    // https://docs.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/yield
    // http://blog.naver.com/rintiantta/40113858529

    //  IEnumerator  기본,  IEnumerable  개선된 타입   
    //  --> IEnumerable (IEnumerator GetEnumerator())을 위해서는 IEnumerator 가 구현되어 있어야 한다     // ★★★★★  // 잘못된 내용
    //  IEnumerator 에는 Object Current { get; }, bool MoveNext(), void Reset() 만 존재한다  
    //  그러므로 MoveNext() 만 사용 가능하다.
    //  foreach를 사용하기 위해서는 IEnumerable 이 구현되어야 한다(IEnumerator GetEnumerator();)  (내용수정) // ★★★★★★
    //  --> 사용자정의 타입에서의 얘기이고 기존 타입에 대해서는 IEnumerable 메서드명(), IEnumerator GetEnumerator() 이든 상관없다
    //      아니면 yield return을 사용하면 될 듯하다
    //      아래 (http://www.csharpstudy.com/CSharp/CSharp-yield.aspx)소스 참조
    
    #region     //  http://www.csharpstudy.com/CSharp/CSharp-yield.aspx 의 소스         
    //  <Example_1>
    //  class Program
    //  {
    //     static IEnumerable<int> GetNumber()
    //     {
    //         yield return 10;  // 첫번째 루프에서 리턴되는 값
    //         yield return 20;  // 두번째 루프에서 리턴되는 값
    //         yield return 30;  // 세번째 루프에서 리턴되는 값
    //     }

    //     static void Main(string[] args)
    //     {
    //         foreach (int num in GetNumber())
    //         {
    //             Console.WriteLine(num);
    //         }             
    //     }
    //  }

    //  <Example_2>
    //  public class MyList
    //  {
    //     private int[] data = { 1, 2, 3, 4, 5 };
        
    //     public IEnumerator GetEnumerator()
    //     {
    //         int i = 0;
    //         while (i < data.Length)
    //         {
    //             yield return data[i];
    //             i++;                
    //         }
    //     }

    //     //...
    //  }

    //  class Program
    //  {
    //     static void Main(string[] args)
    //     {
    //         // (1) foreach 사용하여 Iteration
    //         var list = new MyList();

    //         foreach (var item in list)  
    //         {
    //             Console.WriteLine(item);
    //         }

    //         // (2) 수동 Iteration
    //         IEnumerator it = list.GetEnumerator();
    //         it.MoveNext();
    //         Console.WriteLine(it.Current);  // 1
    //         it.MoveNext();
    //         Console.WriteLine(it.Current);  // 2
    //     }
    //  }
    #endregion

    #region     //  https://docs.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/yield 의 소스
    // public static class GalaxyClass
    // {
    //     public static void ShowGalaxies()
    //     {
    //         var theGalaxies = new Galaxies();
    //         foreach (Galaxy theGalaxy in theGalaxies.NextGalaxy)
    //         {
    //             Debug.WriteLine(theGalaxy.Name + " " + theGalaxy.MegaLightYears.ToString());
    //         }
    //     }

    //     public class Galaxies
    //     {

    //         public System.Collections.Generic.IEnumerable<Galaxy> NextGalaxy
    //         {
    //             get
    //             {
    //                 yield return new Galaxy { Name = "Tadpole", MegaLightYears = 400 };
    //                 yield return new Galaxy { Name = "Pinwheel", MegaLightYears = 25 };
    //                 yield return new Galaxy { Name = "Milky Way", MegaLightYears = 0 };
    //                 yield return new Galaxy { Name = "Andromeda", MegaLightYears = 3 };
    //             }
    //         }
    //     }

    //     public class Galaxy
    //     {
    //         public String Name { get; set; }
    //         public int MegaLightYears { get; set; }
    //     }
    // }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            #region
            // C#의 yield 키워드는 호출자(Caller)에게 컬렉션 데이타를 하나씩 리턴할 때 사용한다. 
            // 흔히 Enumerator(Iterator)라고 불리우는 이러한 기능은 집합적인 데이타셋으로부터 데이타를 하나씩 호출자에게 보내주는 역할을 한다.
            // yield는 yield return 또는 yield break의 2가지 방식으로 사용되는데, 
            // (1) yield return은 컬렉션 데이타를 하나씩 리턴하는데 사용되고, 
            // (2) yield break는 리턴을 중지하고 Iteration 루프를 빠져 나올 때 사용한다.
            // 데이타의 양이 커서 모든 데이타를 한꺼번에 리턴하는 것하는 것 보다 조금씩 리턴하는 것이 더 효율적일 경우 사용

            test1 obj_test1 = new test1();
            //  #####################################################################################################
            //  구현 자체가 IEnumerable<int>, IEnumerator<int> GetEnumerator() 이라도 반환값이 모두 yield return 이므로 
            //  가능할 수 있다. 사용자 정의 타입에서 테스트 해 봐야 한다    // ★★★★★★★★★★
            //  #####################################################################################################

            // IEnumerable 인터페이스의 IEnumerator GetEnumerator()가 있으므로 바로 foreach() 사용가능하다   // ★★★★★★★★            
            // foreach(int a in obj_test1)
            //      Console.WriteLine("a : {0}", a);
            // IEnumerator inner_before --> result : 1, loop_index : 0
            // a : 2
            // IEnumerator inner_before --> result : 2, loop_index : 1
            // a : 4
            // IEnumerator inner_before --> result : 4, loop_index : 2
            // a : 8

            // IEnumerable 이므로 foreach() 사용 가능
            IEnumerable<int> aa = obj_test1.Power(2,4);     //  yield return ~~;    // ★★★★
            //  foreach(int a in aa)
            //     Console.WriteLine("a : {0}", a);

            // IEnumerable 이지만 구현하지도 않은 GetEnumerator()를 사용 할 수 있다     // ★★★★★★
            IEnumerator<int> aa_1 = obj_test1.Power(2,4).GetEnumerator();
            // foreach(int _aa in aa_1)  // 사용 불가 'IEnumerator<int>' 형식 변수에서 foreach 문을 수행할 수 없습니다.        

            // while(aa_1.MoveNext())                       // IEnumerator 이므로 MoveNext()만 사용 가능 // ★★★★★
            //     Console.WriteLine("aa_1 : ", aa_1.Current);
            
            // aa_1.MoveNext();
            // Console.WriteLine("aa_1.MoveNext() 1st : " + aa_1.Current.ToString());
            // aa_1.MoveNext();
            // Console.WriteLine("aa_1.MoveNext() 2nd : " + aa_1.Current.ToString());
            // IEnumerable inner before --> result : 1, loop_index : 0
            // aa_1.MoveNext() 1st : 2
            // IEnumerable inner before --> result : 2, loop_index : 1
            // aa_1.MoveNext() 2nd : 4


            // 반복기에는 ref, in 또는 out 매개 변수를 사용할 수 없습니다       // ★★★★
            //int intReturn = 0;
            //IEnumerable<int> aa = obj_test1.Power(2,8, out intReturn);  
            
            // IEnumerator 이므로 MoveNext() 만 사용 가능         
            IEnumerator<int> bb = aa.GetEnumerator();                   
            // foreach(int _bb in bb)   // 사용불가         // ★★★★

            // while(bb.MoveNext())
            //   Console.WriteLine("bb : " + bb.Current.ToString());            
            // IEnumerable inner before --> result : 1, loop_index : 0
            // bb : 2
            // IEnumerable inner before --> result : 2, loop_index : 1
            // bb : 4
            // IEnumerable inner before --> result : 4, loop_index : 2
            // bb : 8
            // IEnumerable inner before --> result : 8, loop_index : 3
            // bb : 16
     
            // bb.MoveNext();
            // Console.WriteLine(bb.Current.ToString());
            // // 2

            // IEnumerator<int> 변수 = IEnumerable.GetEnumerator();
            // Console.WriteLine(aa.GetEnumerator().Current.ToString());    
            // 0

            // Console.WriteLine (aa.ToString());  // test.test1+<Power>d__0

            // foreach(int ii in obj_test1.Power(2,4))      // IEnumerable 이므로 foreach 사용가능함
            //      Console.WriteLine("{0}",ii);
            // 2   4   8   16  32  64  128  256

            // www.cSharpstudy.com 
            // var aaa = new test1().GetEnumerator(2,4);    // 사용할 수 없다. 인수가 사용될 수 없다
            
            var aaa = new test1();   
            //  자동으로 사용  
            //  IEnumerable의  "IEnumerator<int> GetEnumerator()" 이 있으므로 foreach 사용가능    // ★★★★                  
            // foreach(var _a in aaa)                       
            // {
            //     Console.WriteLine("_a : {0}", _a);      // ★★★★★★
            // }
            // IEnumerator inner_before --> result : 1, loop_index : 0
            // _a : 2
            // IEnumerator inner_before --> result : 2, loop_index : 1
            // _a : 4
            // IEnumerator inner_before --> result : 4, loop_index : 2
            // _a : 8
            // IEnumerator inner_before --> result : 8, loop_index : 3
            // _a : 16

            //  수동으롤 사용
            //  GetEnumerator()를 호출  IEnumerator 이므로  MoveNext() 만 사용됨    // ★★★★  
            IEnumerator<int> iit = aaa.GetEnumerator();              
            // // foreach(int _iit in iit)   //   사용 불가
            // //      Console.WriteLine(_iit.ToString())   ;
            // Console.WriteLine("current : " + iit.Current);     // 0
            // iit.MoveNext();
            // Console.WriteLine("current : " + iit.Current);     // 2
            // iit.MoveNext();
            // Console.WriteLine("current : " + iit.Current);     // 4
            // iit.MoveNext();
            // Console.WriteLine("current : " + iit.Current);     // 8

            // current : 0
            // IEnumerator inner_before --> result : 1, loop_index : 0
            // current : 2
            // IEnumerator inner_before --> result : 2, loop_index : 1
            // current : 4
            // IEnumerator inner_before --> result : 4, loop_index : 2
            // current : 8
            // ---------------------------------
            // current : 0
            // current : 2
            // IEnumerator inner_after --> result : 2, loop_index : 0
            // current : 4
            // IEnumerator inner_after --> result : 4, loop_index : 1
            // current : 8

            // while (iit.MoveNext())
            // Console.WriteLine(iit.Current);
            // 2    4    8    16           

            IEnumerable<int> bbb = new test1().ttor(2,8);
            // foreach(var _bb in bbb)            
            //     Console.WriteLine("_bb : {0}", _bb);            
            // _bb : 2
            // _bb : 4
            // _bb : 8
            // _bb : 16           
            #endregion

            #region 
            // IEnumerable, IEnumerator
            Person[] peopleArray = new Person[3]
            {
                new Person ("John", "Smith"),
                new Person ("Jim",  "Johnson"),
                new Person ("Sue",  "Rabon"),                
            };

            #region
            // People peopleList = new People(peopleArray);
            // foreach(Person p in peopleList)
            //     Console.WriteLine(p.firstName + " " + p.lastName);
            // // John Smith,    Jim Johnson,    Sue Rabon
            
            //PeopleEnum peoEnum = new PeopleEnum(peopleArray);
            // int iidx = 0;
            // while(peoEnum.MoveNext())
            // {
            //     Console.WriteLine("peoEnum : " + peoEnum.Current.firstName + " " + peoEnum.Current.lastName + ", ");
            //     //Console.WriteLine("peoEnum : " + peoEnum._people[iidx].firstName + " " + peoEnum._people[iidx].lastName );  // aabbcc 일때
            //     iidx++;
            // }
            // // peoEnum : John Smith, 
            // // peoEnum : Jim Johnson, 
            // // peoEnum : Sue Rabon, 
            #endregion

            // ##########################################################################################################
            // Collection       : List<T>(.ToList()), Array[T](.ToArray())
            // Sequence         : IEnumerable<T>, IEnumerator<T>   
            // Iterator method  : 요청한 sequence를 생산하기 위해서 yield return 을 사용하는 메서드
            // IEnumerable 인터페이스의 IEnumerator GetEnumerator()를 위해서 IEnumerator 인터페이스가 반드시 존재해야 하는가?
            // 연계되지 않아도 된다.  ★★★★★★
            // ##########################################################################################################

            // IEnumerator 로 구현 되었을 때    // ★★
            // 실제 IEnumerator 에는 Object Current { get; }, bool MoveNext(), void Reset() 만 존재한다             
            Peopleor pList = new Peopleor(peopleArray);
            // foreach(Person p in pList)   // 오류 발생 GetEnumerator 에 대한 공용 인스턴스 정이가 없다고 에러 발생            
            // foreach(var p in pList)      // 오류 발생 GetEnumerator 에 대한 공용 인스턴스 정이가 없다고 에러 발생
            // foreach(string p in pList)   // 오류 발생 GetEnumerator 에 대한 공용 인스턴스 정이가 없다고 에러 발생
            //     Console.WriteLine("p : {0}, {1}", p.firstName, p.lastName);
            //  결론 foreach를 사용하기 위해서는 IEnumerable 이 구현되어야 한다(IEnumerator GetEnumerator();) // ★★★★★★
                        
            // foreach 문 대신에 IEnumerator 에 있는 MoveNext() 는 사용할 수 있다 아래처럼  // ★★★★
            // while (pList.MoveNext())
            //     Console.WriteLine ("position : " + pList._people[pList.position].firstName + " " + pList._people[pList.position].lastName + ", ");
            //     // 객체에 접근하기 위해서 position 을 public 으로 지정했다
            //     // position : John Smith, 
            //     // position : Jim Johnson, 
            //     // position : Sue Rabon, 
            //      Console.WriteLine("current : " + ((Person)pList.Current).firstName + " " + ((Person)(pList.Current)).lastName + ", " );
            //      // object IEnumerator.Current ==> public object Current 로 변경해서 object(객체 전체 : return _people[position])을 가져온다
            //      // current : John Smith, 
            //      // current : Jim Johnson, 
            //      // current : Sue Rabon, 
            //     Console.WriteLine (pList.cur_position.firstName + ", " + pList.cur_position.lastName);    
            //     // 현재 위치(cur_position)로 접근하기 위한 뭔가 있어야 한다. 아래처럼 object 에 대한 접근 및 cast는 되지 않는다. // ★★★★
            //     // Current 는 객체 자신이므로 객체안의 해당 instance에 대한 접근을 위해서
            //     // current는 관련 또는 current를 포함해서 해당 instance에 접근할 수 있는 메서드가 따로 존재해야 한다. // ★★★★★★
            //     // current : Peopleor의 public Person cur_position --> return _people[position] 
            //     //           PeopleEnum 의 public Person Current --> return  _people[position] 
            //     Console.WriteLine("pList : " + pList._people[pList.position].firstName + " " + pList._people[pList.position].lastName + ", ");
            // // pList : John Smith, 
            // // pList : Jim Johnson, 
            // // pList : Sue Rabon, 

            // IEnumerable 로 구현 되었을 때    // ★★★★★★★ (foreach문 사용 하기 위해서)
            // IEnumerable 에는 IEnumerator GetEnumerator() 만 존재한다.    
            Peopleable pableList = new Peopleable(peopleArray);
            foreach(Person p in pableList)
                Console.WriteLine(p.firstName + ", " + p.lastName);
            // John Smith,   Jim Johnson,    Sue Rabon
            
            // ★★★   에러 발생
            // 'object'에는 'firstName'에 대한 정의가 포함되어 있지 않고, 
            // 'object' 형식의 첫 번째 인수를 허용하는 액세스 가능한 확장 메서드 'firstName'이(가) 없습니다             
            // foreach(var p in pableList) 
            //     //Console.WriteLine( p.firstName + ", " + p.lastName);                      // 에러 발생
            //     Console.WriteLine( ((Person)p).firstName + ", " + ((Person)p).lastName);    // OK, 'p'에 대한 명시적 형변환으로 사용 가능해짐

            // IEnumerable, IEnumerator 둘다 한 클래스에 구현되었을 때
            Peoplemulti pmultiList = new Peoplemulti(peopleArray);
            // foreach(Person obj in pmultiList)    // pmultiList 는 현재 객체(object IEnumerator.Current --> return Person[position])  // ★★★★                 
            //    Console.WriteLine("this 1 : " + obj.firstName + " " + obj.lastName);
            // // this 1 : John Smith
            // // this 1 : Jim Johnson
            // // this 1 : Sue Rabon

            // foreach(Object obj2 in pmultiList)                                      
            //     Console.WriteLine("this 2 : " +  ((Person)obj2).firstName); 
            // // this 2 : John
            // // this 2 : Jim
            // // this 2 : Sue

            // IEnumerator, IEnumerable 로 구현 되었을 때
            LectureRoom lr = new LectureRoom();
            lr.AddTutee( new Tutee("홍길동"));
            lr.AddTutee( new Tutee("강감찬"));
            //lr.InTutor( new Tutor("설민석"));            
            //lr.InTutor( new Tutor("한용운"));
            lr.AddTutee( new Tutee("이성계"));
            lr.AddTutee( new Tutee("이방원"));

            // foreach(object obj in lr)
            //      Console.WriteLine(obj);

            #endregion
        
            #region     // Generic  // ★★★★
            // ConvertAll
            // List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter);
            // 현재 List<T>의 요소를 다른 형식으로 변환하고 변환된 요소를 포함하는 목록을 반환합니다.

            // Converter 대리자
            // public delegate TOutput Converter<in TInput, out TOutput>(TInput input);
            // 개체를 한 형식에서 다른 형식으로 변환하는 메서드를 나타냅니다.

            PointF[] arrPF = {  new PointF(27.8F, 32.62F),
                                new PointF(99.3F, 147.273F),
                                new PointF(7.5F, 1412.2F)
                             };
            // foreach(PointF p in arrPF)                             
            //     Console.WriteLine(p);

            // ★★★ 
            // 원래 메서드 ConvertAll() 에 대해서  Array, List 모두 다르게 표현된다, 메서드 구현 전, 후 가 다르다

            // Array.ConvertAll 의 Intellisense : TOutput[] ConvertAll(TInput[] array, Converter<TInput, TOutput> converter)   // 메서드 작성 전           
            // TOutput[] Array.ConvertAll<TInput, TOutput>(TInput[] array, Converter<TInput, TOutput> converter)               // 메서드 작성 후
            Point[] ap = Array.ConvertAll(arrPF, new Converter<PointF, Point>(PointFToPoint) );
            // foreach(Point pp in ap)
            //     Console.WriteLine("pp : " + pp);
            // // pp : {X=27,Y=32}
            // // pp : {X=99,Y=147}
            // // pp : {X=7,Y=1412}

            List<PointF> listPF = new List<PointF>() {  new PointF(27.8F, 32.62F),
                                                        new PointF(99.3F, 147.273F),
                                                        new PointF(7.5F, 1412.2F)
                                                     };            

            // listPF.ConvertAll 의 Intellisense : List<TOutput> ConvertAll(Converter<PointF, TOutput> converter)   // 메서드 작성 전            
            // List<Point> List<PointF>.ConvertAll<Point>(Converter<PointF, Point> converter)                       // 메서드 작성 후
            List<Point> pointList = listPF.ConvertAll(new Converter<PointF, Point>(PointFToPoint));
            
            // foreach(Point aaa in pointList)
            //     Console.WriteLine("aaa : " + aaa);
            // // aaa : {X=27,Y=32}
            // // aaa : {X=99,Y=147}
            // // aaa : {X=7,Y=1412}

            #endregion

            #region     // https://docs.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/yield

            var theGalaxies_able = new Galaxies_able();
            // foreach (Galaxy theGalaxy in theGalaxies_able.NextGalaxy())
            // {
            //     Console.WriteLine(theGalaxy.Name + " " + theGalaxy.MegaLightYears.ToString());
            // }
            var theGalaxies_able2 = new Galaxies_able2();
            // foreach (Galaxy theGalaxy in theGalaxies_able2.NextGalaxy)
            // {
            //     Console.WriteLine(theGalaxy.Name + " " + theGalaxy.MegaLightYears.ToString());
            // }

            var theGalaxies_or = new Galaxies_or();
            // foreach(Galaxy theGalaxy in theGalaxies_or)                  // ★★★★★  사용법
            // {
            //     Console.WriteLine(theGalaxy.Name + " " + theGalaxy.MegaLightYears.ToString());
            // }            

            // Tadpole 400
            // Pinwheel 25
            // Milky Way 0
            // Andromeda 3

            IEnumerator theGalaxy_or2 = theGalaxies_or.GetEnumerator();     // ★★★★★  사용법
            theGalaxy_or2.MoveNext();
            // Console.WriteLine(theGalaxy_or2.Current);
            // Galaxy obj_galaxy = (Galaxy)theGalaxy_or2.Current;
            // Console.WriteLine("Name : {0}, MegaLightYears : {1} ",obj_galaxy.Name, obj_galaxy.MegaLightYears );
            // Console.WriteLine("Name : {0}, MegaLightYears : {1} ",((Galaxy)theGalaxy_or2.Current).Name, ((Galaxy)theGalaxy_or2.Current).MegaLightYears);

            // test.Galaxy
            // Name : Tadpole, MegaLightYears : 400 
            // Name : Tadpole, MegaLightYears : 400 
            #endregion
        
            #region     // http://blog.naver.com/rintiantta/40113858529         // 아래 예제 매우 중요

                YieldTest yt = new YieldTest();

                // foreach(var item in yt)                                  // OK
                //     Console.WriteLine(item);                
                                
                // IEnumerator testor = yt.GetEnumerator();                 // NO  // 실핻불가     
                // // 'IEnumerator' 형식 변수에서 foreach 문을 수행할 수 없습니다
                // // 그런데 위에서 실행이 되는 것은 내부적으로 IEnumerable로 변형 되기 때문이란다     // ★★★★★
                // // foreach(var item in testor)
                // //     Console.WriteLine(item);

                // foreach(var item in yt.OtherGetEnum())
                //     Console.WriteLine(item);
                
                // IEnumerable testable = yt.OtherGetEnum();
                // foreach(var item in testable)
                //     Console.WriteLine(item);

                // -----------------------------------------------------------------------------------------------------

                YieldTest_or ytor = new YieldTest_or();     
                // foreach(var item in ytor)                               // 자체가 IEnumerable 이다      // ★★★★★
                //     Console.WriteLine(item);                            // OK

                IEnumerator ytor_obj = ytor.GetEnumerator();
                //  'IEnumerator' 형식 변수에서 foreach 문을 수행할 수 없습니다. 
                //  'IEnumerator'에는 'GetEnumerator'의 공개 인스턴스 또는 확장 정의가 없기 때문입니다.
                //  즉 IEnumerator 에는 Object Current{ get }, bool MoveNext(), void Reset() 이 존재해야 하기 때문일 것 같다
                // foreach(var item in ytor_obj)
                //     Console.WriteLine(item);                         // NO

                YieldTest_able ytable = new YieldTest_able();   

                // foreach(var item in ytable.OtherGetEnum())           // OK
                //     Console.WriteLine(item);    
                
                // IEnumerable ytable_obj = ytable.OtherGetEnum();      // OK
                // foreach(var item in ytable_obj)
                //     Console.WriteLine(item);

                // foreach(var item in ytable)                          // NO   // 자체가 IEnumerable 은 아니다  // ★★★★★           
                //     Console.WriteLine(item);    
                //  'YieldTest_able' 형식 변수에서 foreach 문을 수행할 수 없습니다. 
                //  'YieldTest_able'에는 'GetEnumerator'의 공개 인스턴스 또는 확장 정의가 없기 때문입니다.

                YieldTest_Ior ytIor = new YieldTest_Ior();              // IEnumerator 를 구현
                // foreach(var item in ytIor)                           // NO
                //     Console.WriteLine(item);
                //  'YieldTest_Ior' 형식 변수에서 foreach 문을 수행할 수 없습니다. 
                //  'YieldTest_Ior'에는 'GetEnumerator'에 대한 공용 인스턴스 정의가 없기 때문입니다.
                
                while(ytIor.MoveNext())                
                    Console.WriteLine(ytIor.Current);           

                // ########################### 최종 결론 20201111 ########################################
                // IEnumerator 만을 가지고는 Iterator(foraech) 를 구현 할 수가 없다
                // IEnumerator 를 가지고는 MoveNext()를 실행 할 수 있다
                // IEnumerable (IEnumerator GetEnumerator())가 있어야 Iterator(foreach)를 구현 할 수 있다
                // 리턴은 yield return 이어야 하며
                // IEnumerator GetEnumerator() { return 객체.GetEnumerator() }; 도 가능하다
                // ######################################################################################

            #endregion 

            #region     // http://www.csharpstudy.com/CSharp/CSharp-yield.aspx
            
            csharp_able csharp_able = new csharp_able();            // 클래스 자체가 IEnumerable이 아니다
            // foreach(var item in csharp_able.GetNumber())         // OK   // 클래스 자체가 IEnumerable 이 아니므로 IEnumerable를 리턴하는 메서드 호출
            //     Console.WriteLine(item);
            
            // foreach(var item in csharp_able)                     // NO   // 클래스 자체가 IEnumerable 이 아니므로 에러 발생
            //     Console.WriteLine(item);
            // 'csharp_1' 형식 변수에서 foreach 문을 수행할 수 없습니다. 
            // 'csharp_1'에는 'GetEnumerator'의 공개 인스턴스 또는 확장 정의가 없기 때문입니다.

            csharp_or_able csharp_or_able = new csharp_or_able();      // 클래스 자체가 IEnumerable 이다
            // foreach(var item in csharp_or_able)                     // OK    // 클래스 자체가 IEnumerable 이므로 가능
            //     Console.WriteLine(item);
            
            // foreach(var item in csharp_or_able.GetEnumerator())     // NO    
            //  'IEnumerator' 형식 변수에서 foreach 문을 수행할 수 없습니다. 
            //  'IEnumerator'에는 'GetEnumerator'에 대한 공용 인스턴스 정의가 없기 때문입니다. 

            // IEnumerator 를 리턴하므로 MoveNext()도 가능하다
            IEnumerator tt = csharp_or_able.GetEnumerator();
            // tt.MoveNext();
            //     Console.WriteLine("Current : " + tt.Current);

            #endregion
        }
        public static Point PointFToPoint(PointF pf)
        {
            return new Point( ((int)pf.X), ((int)pf.Y) );
        }                
    }
    
    #region     // https://docs.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/yield
    // http://www.csharpstudy.com/CSharp/CSharp-yield.aspx
    // https://docs.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/yield
    // 해당 소스 관련

    public class Galaxies_able
    {
        public System.Collections.Generic.IEnumerable<Galaxy> NextGalaxy()          // method
        {
            //get
            //{
                yield return new Galaxy { Name = "Tadpole", MegaLightYears = 400 };
                yield return new Galaxy { Name = "Pinwheel", MegaLightYears = 25 };
                yield return new Galaxy { Name = "Milky Way", MegaLightYears = 0 };
                yield return new Galaxy { Name = "Andromeda", MegaLightYears = 3 };
            //}
        }
    }

    public class Galaxies_able2
    {
        public System.Collections.Generic.IEnumerable<Galaxy> NextGalaxy            // property
        {
            get
            {
                yield return new Galaxy { Name = "Tadpole", MegaLightYears = 400 };
                yield return new Galaxy { Name = "Pinwheel", MegaLightYears = 25 };
                yield return new Galaxy { Name = "Milky Way", MegaLightYears = 0 };
                yield return new Galaxy { Name = "Andromeda", MegaLightYears = 3 };
            }
        }
    }

    public class Galaxies_or
    {
        // public System.Collections.Generic.IEnumerator<Galaxy> NetGalaxy
        // IEnumerator<Galaxy>'에는 'GetEnumerator'에 대한 공용 인스턴스 정의가 없기 때문입니다.
        public System.Collections.Generic.IEnumerator<Galaxy> GetEnumerator()
        {
            // get
            // {
                yield return new Galaxy { Name = "Tadpole", MegaLightYears = 400 };
                yield return new Galaxy { Name = "Pinwheel", MegaLightYears = 25 };
                yield return new Galaxy { Name = "Milky Way", MegaLightYears = 0 };
                yield return new Galaxy { Name = "Andromeda", MegaLightYears = 3 };
            //}
        }
    }    

    public class Galaxy
    {
        public String Name { get; set; }
        public int MegaLightYears { get; set; }
    }
    #endregion

    #region     // http://blog.naver.com/rintiantta/40113858529

    public class YieldTest
    {
        string[] days = {"Sun", "Mon", "Tue", "Wed", "Thr", "Fri", "Sat"};

        public IEnumerator GetEnumerator()  // return day 일때  // NO  // 'YieldTest.GetEnumerator()': 코드 경로 중 일부만 값을 반환합니다.
        {
            foreach(var day in days)
            {
                yield return day;
                // return day;      // NO       // ★★★★★★  
                                    // 암시적으로 'string' 형식을 'System.Collections.IEnumerator' 형식으로 변환할 수 없습니다
            }
        }

        public IEnumerable OtherGetEnum()
        {
            for (int i = 0; i < days.Length; i++)
            {
                yield return days[i];
            }
        }
    }

    public class YieldTest_or
    {
        string[] days = {"Sun", "Mon", "Tue", "Wed", "Thr", "Fri", "Sat"};

        public IEnumerator GetEnumerator()      // return day 일때  // NO  // 'YieldTest_or.GetEnumerator()': 코드 경로 중 일부만 값을 반환합니다.
        {
            foreach(var day in days)
            {
                yield return day;
                //return day;           // NO     // 암시적으로 'string' 형식을 'System.Collections.IEnumerator' 형식으로 변환할 수 없습니다.
            }
        }
    }

    public class YieldTest_able
    {
        string[] days = {"Sun", "Mon", "Tue", "Wed", "Thr", "Fri", "Sat"};

        public IEnumerable OtherGetEnum()   // return days[i] 일때  // NO  // 'YieldTest_able.OtherGetEnum()': 코드 경로 중 일부만 값을 반환합니다.
        {
            for (int i = 0; i < days.Length; i++)
            {
                yield return days[i];
                // return days[i];         // NO   
            }
        }
    }

    class YieldTest_Ior //: IEnumerator        // IEnumerator 를 구현해 보자
    {
        string[] days = {"Sun", "Mon", "Tue", "Wed", "Thr", "Fri", "Sat"};
        
        int position = -1;

        // bool IEnumerator.MoveNext()
        public bool MoveNext()
        {
            position++;
            return (position < days.Length);
        }

        // void IEnumerator.Reset() 
        public void Reset()
        {
            position = -1;
        }

        public Object Current
        {
            get 
            {
                try
                {
                   return days[position];
                   // yield return _people[position];  // 사용 불가     // ★★★★★★★  이것 때문에 foreach를 사용할 수 없을 수도...
                   // --> catch 절이 포함된 try 블록의 본문에서는 값을 생성할 수 없습니다.
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }               
                //yield return days[position];
            }
        }
    }

    #endregion 
    
    #region     // http://www.csharpstudy.com/CSharp/CSharp-yield.aspx

    public class csharp_able
    {
        public IEnumerable<int> GetNumber()
        {
            yield return 10;  // 첫번째 루프에서 리턴되는 값
            yield return 20;  // 두번째 루프에서 리턴되는 값
            yield return 30;  // 세번째 루프에서 리턴되는 값
            //  return 10;      // NO   암시적으로 'int' 형식을 'System.Collections.Generic.IEnumerable<int>' 형식으로 변환할 수 없습니다.
        }
    }

    public class csharp_or_able
    {
        private int[] data = { 11, 22, 33, 44, 55 };
    
        public IEnumerator GetEnumerator()  // NO   // return 일때  // 'csharp_or_able.GetEnumerator()': 코드 경로 중 일부만 값을 반환합니다.
        {
            int i = 0;
            while (i < data.Length)
            {
                yield return data[i];
                //  return data[i];         // NO   // 암시적으로 'int' 형식을 'System.Collections.IEnumerator' 형식으로 변환할 수 없습니다. 
                i++;                
            }
        }
    }
    #endregion 

    public class test1
    {
        // public IEnumerator<int> GetEnumerator(int number, int exponent)         // 사용 안됨  ★★★★
        // IEnumerable 인터페이스의 구현에 해당되는 IEnumerator<T> GetEnumerator()가 있으므로 바로 foreach() 사용 가능 // ★★★★★★
        public IEnumerator<int> GetEnumerator()     // 이렇게만 가능 : IEnumerator<T> --> IEnumerable 인터페이스의 구현에 해당된다
        {
            int result = 1;
            
            //int idx = 0; 
            int number = 2;
            int exponent = 4;
            
            // 둘 다 가능
            for(int i = 0; i < exponent; i++)
            {
                Console.WriteLine("IEnumerator inner_before --> result : {0}, loop_index : {1}", result, i);
                result = result * number;
                yield return result;        
                //Console.WriteLine("IEnumerator inner_after --> result : {0}, loop_index : {1}", result, i);

            } 
            // while(idx++ < exponent)
            // {
            //     result = result * number;
            //     yield return result;
            // }
        }

        //public IEnumerable<int> Power(int number, int exponent, out int retIndex)    // 불가               
        public IEnumerable<int> Power(int number, int exponent)
        {
            int result = 1;
            
            //int j = 0;
            //retIndex = j;
            //for (int i = j; i< exponent; i++)

            for (int i = 0; i< exponent; i++)
            {
                Console.WriteLine("IEnumerable inner before --> result : {0}, loop_index : {1}", result, i);
                result = result * number;
                yield return result;
                //Console.WriteLine("IEnumerable inner after --> result : {0}, loop_index : {1}", result, i);
            }
        }
        public IEnumerable<int> ttor (int number, int exponent)     // OK
        // public IEnumerator<int> ttor(int number, int exponent)   // 사용불가
        // 'IEnumerator<int>' 형식 변수에서 foreach 문을 수행할 수 없습니다. 
        // 'IEnumerator<int>'에는 'GetEnumerator'에 대한 공용 인스턴스 정의가 없기 때문입니다.    
        // 
        //  ###########    사용 가능형식 ★★★★★     ##########
        //  IEnumerable<T> 메서드명(파라미터, 파라미터)
        //  IEnumerator<T> GetEnumerator()              // 고정되어 있다
        //  기존 타입과 사용자 타입과는 다르다
        //  기존 타입에는 이미 IEnumerable 과 IEnumerator 가 구현되어 있으나
        //  사용자 타입에는 IEnumerable 의 IEnumerator GetEnumerator() 에서 IEnumerator를 호출하기 때문에
        //  IEnumerable 의 IEnumerator GetEnumerator() 를 위해서 IEnumerator 인터페이스가 구현되야만 한다   // ★★★★★★
        //  ####################################################
        {
            int result = 1;
            
            for (int i = 0; i< exponent; i++)
            {
                result = result * number;
                yield return result;  
            }
        }
    }

    #region     // IEnumerable, IEnumerator

    public class Person
    {
        // public string firstName;
        // public string lastName;
        public string firstName { get; set; }
        public string lastName { get; set; }

        public Person(string fName, string lName)
        {
            firstName = fName;
            lastName = lName;
        }
    }

    #region // https://docs.microsoft.com/ko-kr/dotnet/api/system.collections.ienumerator?view=netframework-4.8  

    public class People : IEnumerable
    {
        private Person[] _people;

        public People(Person[] pArray)
        {
            _people = new Person[pArray.Length];
            for (int i = 0 ; i < pArray.Length; i++)
            {
                _people[i] = pArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()     // 인터페이스의 명시적 호출 지정자 사용하지 않는다
        {
            //return (IEnumerator) GetEnumerator();   // Definition(정의) : 아랫 부분
            //return new PeopleEnum(_people);       // 둘 다 사용 가능하다
            //yield return new PeopleEnum(_people);   // IEnumerator 에서는 (yield return) 을 사용 할 수 없다           // ★★★★★★
            //yield return new Person(_people[i].firstName )  // 이렇게는 사용되지만 어떤 index인지 몰라서 사용 불가 이다 // ★★★★★★
            
            return _people.GetEnumerator();     // IEnumerator 로 연계없이 IEnumerable에서 단독으로 완성될 수 있다. // ★★★★★★★★★★            
            // yield return _people.GetEnumerator();     // 이것도 가능하다 
        }

        public PeopleEnum GetEnumerator()
        {
            return new PeopleEnum(_people);         // 이 곳에서 다시 IEnumerator 관련 부분을 호출한다  // ★★★★★★            
        }
    }

    public class PeopleEnum : IEnumerator
    {
        public Person[] _people;

        int position = -1;

        public PeopleEnum(Person[] list)
        {
            _people = list;
        }

        // bool IEnumerator.MoveNext()
        public bool MoveNext()
        {
            position++;
            return (position < _people.Length);
        }

        // void IEnumerator.Reset() 
        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;           // 정의 : 아래 부분
                // return _people[position];   // aabbcc 일때

                //  사용 불가   // ★★★★★
                //  yield return Current;                           // 사용될 수 없다
                //  object'이(가) 반복기 인터페이스 형식이 아니므로 
                //  'PeopleEnum.IEnumerator.Current.get'의 본문은 반복기 블록이 될 수 없습니다.
                //  yield return _people[position];
            }
        }

        public Person Current
        {
            get 
            {
                try
                {
                   return _people[position];
                   // yield return _people[position];  // 사용 불가
                   // --> catch 절이 포함된 try 블록의 본문에서는 값을 생성할 수 없습니다.
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }

                // 모두 실패    // ★★★★★
                //  yield return _people[position];
                //  yield return new Person(_people[position].firstName , _people[position].lastName);
                //  yield return new Person(_people[position].firstName, _people[position].lastName);
                //  --> Person'이(가) 반복기 인터페이스 형식이 아니므로 'PeopleEnum.Current.get'의 본문은 반복기 블록이 될 수 없습니다.
                //  https://docs.microsoft.com/ko-kr/dotnet/csharp/iterators 
            }
        }
    }

    #endregion

    #region
    public class Peopleor : IEnumerator
    {
        public Person[] _people;

        public Peopleor (Person[] _personArray)
        {
            _people = _personArray;
        }

        // public Person cur_position
        // {
        //     get 
        //     {
        //         return _people[position];
        //     }
        // }

        #region     // IEnumerator 구현해야 할 멤버

        public int position = -1;

        // bool IEnumerator.MoveNext()  // ★★★
        public bool MoveNext()
        {
            position++;
            return (position < _people.Length);
        }

        // void IEnumerator.Reset()     // ★★★
        public void Reset()
        {
            position = -1;
        }

        // object IEnumerator.Current   // 적을때 반드시 인터페이스를 표시해야 한다 (1st)   // ★★★★
        public object Current           // (2nd) 위 아래 두 가지 표현법을 모두 알아야 한다  // ★★★★
            {
            get
            {
                //return cur_position;      // ok     <--  위의 cur_position 을 사용할때만
                return _people[position];   // ok
            }
        }

        #endregion
    }

    public class Peopleable : IEnumerable  
    {
        public Person[] tt;

        public Peopleable (Person[] _personArray)
        {
            tt = _personArray;
        }

        #region     // 사용법 1
        // IEnumerator IEnumerable.GetEnumerator()
        // {
        //     return (IEnumerator)_GetEnumerator(); 
        // }

        // public Peopleor _GetEnumerator()    // 이 곳을 통해서 IEnumerator를 상속한 클래스와 연결한다
        // {
        //      return new Peopleor (tt);
        // }
        #endregion

        #region     // 사용법 2
        // 그렇다면 바로 IEnumerator 를 여기에 구현한다면
        // http://ehpub.co.kr/c-8-2-1-ienumerable-ienumerator-%EC%9D%B8%ED%84%B0%ED%8E%98%EC%9D%B4%EC%8A%A4/
        IEnumerator IEnumerable.GetEnumerator()        
        {
            return tt.GetEnumerator();    
        }   
        #endregion             
    }
    
    public class Peoplemulti : IEnumerable, IEnumerator
    {
        public Person[] tt;
        public Peoplemulti (Person[] _personArray)
        {
            tt = _personArray;
        }

        #region     // IEnumerator 구현해야 할 멤버

        int position = -1;

        public bool MoveNext()
        {
            position++;
            return (position < tt.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current  // 적을때 반드시 인터페이스를 표시해야 한다
        {
            get
            {
                //return cur_position;      // ok     <--  위의 cur_position 을 사용할때만
                return tt[position];        // ok
            }
        }

        #endregion

        IEnumerator IEnumerable.GetEnumerator() 
        {
            return this;        // this는 현재 객체 (위의 object IEnumerator.Current)를 말한다. 
        }

    }
    #endregion

    #endregion

    #region     // http://ehpub.co.kr/c-8-2-1-ienumerable-ienumerator-%EC%9D%B8%ED%84%B0%ED%8E%98%EC%9D%B4%EC%8A%A4/          

    class Tutee
    {
        public string Name     {get; private set;}
        
        public Tutee(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return string.Format("수강생 이름 :{0}", Name);
        }
    }

    class Tutor
    {
        public string Name  {   get; private set;   }

        public Tutor(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return string.Format("강사 이름 : {0}", Name);
        }
    }

    class LectureRoom : IEnumerable, IEnumerator
    {
        Tutor tutor = null;

        ArrayList tutees = new ArrayList();
        public void AddTutee(Tutee tutee)
        {
            tutees.Add(tutee);
        }

        public bool InTutor(Tutor tutor)
        {
            if (this.tutor == null)
            {
                this.tutor = tutor;
                return true;
            }
            return false;            
        }

        // IEnumrable, IEnumerator 관련

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;        // ???? 자기 자신을 IEnumerator 한다는 것은 ?????
        }

        int now;

        public LectureRoom()
        {
            Reset();
        }

        public void Reset()
        {
            now = -1;
        }

        public bool MoveNext()
        {
            now++;

            if (tutor == null)              // 현재 강사가 없으면
            {
                if (now < tutees.Count)
                    return true;
                
                Reset();                    // 현재 위치 재설정
                return false;
            }

            // 강사가 있으면
            if ( now <= tutees.Count)
                return true;

            Reset();
            return false;            
        }

        public object Current
        {
            get 
            {
                if (tutor == null)          // 강사가 없다
                {
                    return tutees[now];     // 학생 반환
                }
                // 강사가 있다
                if (now == 0)
                {
                    return tutor;           // 강사 반환
                }
                return tutees[now -1];      // 강사가 있고 교실이 첫번째가 아니면 학생 반환
            }
        }
    }


    #endregion
    
}
