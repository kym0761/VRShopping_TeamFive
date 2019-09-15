# UOS Senier Project

VR SHOPPING

이 프로젝트는 서울시립대 컴퓨터과학부 졸업 작품 VR SHOPPING 프로젝트입니다.

해당 프로젝트는 OS: Windows 10, cpu : i5-2500, graphic card: GTX 960, RAM 8GB 환경에서 개발되었습니다.

이 프로젝트는 E커머스 11번가 API를 받아서 DATA를 받아 유니티 월드상으로 구현하는 내용을 담고 있습니다.

해당 코드의 설명은 "XMLTest"라는 이름의 Script안에 자세히 써져있으므로 참고하시기 바랍니다. (이 코드는 Assets/Script 폴더 안에 존재합니다.)

# 주의

이 프로젝트는 개발 환경 문제상의 이유로 인해, VR 기기를 사용하는 것을 염두하고 개발하였지만, 컨트롤러가 없는 관계로 UI의 기능을 전혀 사용 할 수 없어

VR SDK의 내용을 전부 지우고 컴퓨터 마우스 키보드 환경에서 시연할 수 있도록 만들었습니다.

사용했던 VR기기는 Oculus DK2로, 연구실에서 교수님께 대여를 받아 사용하였습니다.

1. https://www.oculus.com/setup/ 이 주소에 들어가 Rift 소프트웨어를 다운받아 설치

2. https://developer.oculus.com/downloads/ 이 주소에서 Unity SDK를 다운받아 Unity 프로젝트에 적용

이 단계를 거친 뒤에, Player에 관련된 Script를 Oculus 환경에 맞게 적용하면 사용이 가능한 프로젝트입니다.


~~해당 프로젝트는 "개발 환경이 아닌 컴퓨터"에서 UnityEngine.WWW.EscapeURL(toFind,System.Text.Encoding.GetEncoding("euc-kr"))를 사용하여도 EUC-KR로 인코딩이 변경되지 않는 문제점이 발견되었습니다.~~

~~현재 이 문제점은 개발자 본인도 알 수 없으니 양해 부탁드립니다. (5~6월 동안 문제점을 해결하기 위해 애써봤지만 해결법을 찾을 수 없었습니다.)~~

2019년 9월 15일 문제점 수정. 바뀐 개발 환경에서도 한글 깨짐이 발생하지 않음을 확인했습니다.
