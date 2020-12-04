using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace Iterator
{

    // https://www.csharpstudy.com/CSharp/CSharp-yield.aspx
    // IEnumerator 인터페이스는 Current (속성), MoveNext() (메서드), Reset() (메서드) 
    // https://docs.microsoft.com/ko-kr/dotnet/api/system.collections.ienumerator?view=netframework-4.8

    // public interface IEnumerable
    // {
    //     IEnumerator GetEnumerator();
    // }

    // public interface IEnumerator
    // {
    //     object Current { get; }
    //     bool MoveNext();
    //     void Reset();
    // }
    
    //https://msdn.microsoft.com/ko-kr/library/65zzykke(v=vs.100).aspx


    class Program
    {
        static void Main(string[] args)
        {
             test1 obj_test1 = new test1();
            IEnumerable<int> aa = obj_test1.Power(2,8);            
            //obj_test1.Power(2,8).GetEnumerator();

            // 반복기에는 ref, in 또는 out 매개 변수를 사용할 수 없습니다
            //int intReturn = 0;
            //IEnumerable<int> aa = obj_test1.Power(2,8, out intReturn);  

            IEnumerator<int> bb = aa.GetEnumerator();
            
            //while(bb.MoveNext())
            //    Console.WriteLine(bb.Current.ToString());            
            // 2
            // 4
            // 8
            // 16
            // 32
            // 64
            // 128
            // 256

            //bb.MoveNext();
            //Console.WriteLine(bb.Current.ToString());
            // 2

            //Console.WriteLine(aa.GetEnumerator().Current.ToString());
            // 0

            //Console.WriteLine (aa.ToString());

            // foreach(int ii in obj_test1.Power(2,8))
            //      Console.WriteLine("{0}",ii);
            // 2
            // 4
            // 8
            // 16
            // 32
            // 64
            // 128
            // 256
            
            // IEnumerable, IEnumerator
            Person[] peopleArray = new Person[3]
            {
                new Person("John", "Smith"),
                new Person("Jim", "Johnson"),
                new Person("Sue","Rabon"),                
            };

            People peopleList = new People(peopleArray);
            foreach(Person p in peopleList)
                Console.WriteLine(p.firstName + " " + p.lastName);
            // John Smith
            // Jim Johnson
            // Sue Rabon

            // IEnumerator 로 구현 되었을 때
            // 실제 IEnumerator 에는 Object Current { get; }, bool MoveNext(), void Reset() 만 존재한다
            Peopleor pList = new Peopleor(peopleArray);
            // foreach(Person p in pList)   // 오류 발생 GetEnumerator 에 대한 공용 인스턴스 정이가 없다고 에러 발생
            // foreach(string p in pList)
            while (pList.MoveNext())
                Console.WriteLine (pList.cur_position.firstName + " " + pList.cur_position.lastName);
            // John Smith
            // Jim Johnson
            // Sue Rabon

            // IEnumerable 로 구현 되었을 때 
            // IEnumerable 에는 IEnumerator GetEnumerator() 만 존재한다
            Peopleable pableList = new Peopleable(peopleArray);
            //foreach(Person p in pableList)
            //    Console.WriteLine(p.firstName + " " + p.lastName);
            // John Smith
            // Jim Johnson
            // Sue Rabon            
            
            // IEnumerable, IEnumerator 둘다 한 클래스에 구현되었을 때
            Peoplemulti pmultiList = new Peoplemulti(peopleArray);
            //foreach(Person obj in pmultiList)                     
            //    Console.WriteLine("this 1 : " + obj.firstName + " " + obj.lastName);
                 
            foreach(Object obj2 in pmultiList)                                      
                Console.WriteLine("this 2 : " +  obj2.GetType().firstName); 

            // IEnumerator, IEnumerable 로 구현 되었을 때
            LectureRoom lr = new LectureRoom();
            lr.AddTutee( new Tutee("홍길동"));
            lr.AddTutee( new Tutee("강감찬"));
            //lr.InTutor( new Tutor("설민석"));            
            //lr.InTutor( new Tutor("한용운"));
            lr.AddTutee( new Tutee("이성계"));
            lr.AddTutee( new Tutee("이방원"));

            foreach(object obj in lr)
                Console.WriteLine(obj);

        }

        // public static IEnumerable<int> Power(int number, int exponent)  // 원본
        // public IEnumerable<int> Power(int number, int exponent)
        // {
        //     int result = 1;

        //     for (int i =0; i< exponent; i++)
        //     {
        //         result = result * number;
        //         yield return result;
        //     }
        // }
    }

    public class test1
    {
        //public IEnumerable<int> Power(int number, int exponent, out int retIndex)             

        public IEnumerable<int> Power(int number, int exponent)
        {
            int result = 1;
            
            //int j = 0;
            //retIndex = j;
            //for (int i = j; i< exponent; i++)

            for (int i = 0; i< exponent; i++)
            {
                result = result * number;
                yield return result;
            }
        }
    }

    #region // IEnumerable, IEnumerator

    public class Person
    {
        public string firstName;
        public string lastName;

        public Person(string fName, string lName)
        {
            firstName = fName;
            lastName = lName;
        }
    }

    #region 

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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }

        public PeopleEnum GetEnumerator()
        {
            return new PeopleEnum(_people);
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

        public bool MoveNext()
        {
            position++;
            return (position < _people.Length);
        }
         public void Reset()
         {
             position = -1;
         }

         object IEnumerator.Current
         {
            get
            {
                return Current;
            }
         }

         public Person Current
         {
             get 
             {
                 try
                 {
                     return _people[position];
                 }
                 catch (IndexOutOfRangeException)
                 {
                     throw new InvalidOperationException();
                 }
             }
         }
    }

    #endregion

    public class Peopleor : IEnumerator
    {
        public Person[] _people;

        public Peopleor (Person[] _personArray)
        {
            _people = _personArray;
        }

        public Person cur_position
        {
            get 
            {
                return _people[position];
            }
        }

        #region     // IEnumerator 구현해야 할 멤버

        int position = -1;

        public bool MoveNext()
        {
            position++;
            return (position < _people.Length);
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
                return tt[position];   // ok
            }
        }

        #endregion

        IEnumerator IEnumerable.GetEnumerator() 
        {
            return this;
        }

    }
    #endregion

    #region // https://docs.microsoft.com/ko-kr/dotnet/api/system.collections.ienumerator?view=netframework-4.8
    
    class Tutee
    {
        public string Name
        {get; private set;}
        
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
        public string Name
        {   get; private set;   }

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
