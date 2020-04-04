# SDF
https://aras-p.info/blog/2017/02/15/Font-Rendering-is-Getting-Interesting/

- 크기에 따른 폰트 선명도 차이 등을 완화하기 위하여

## Bitmap Font Generator
http://www.angelcode.com/products/bmfont/
- BMFont64
- 세팅에서 A R G B 순으로 각 채널에 담을 내용을 설정할 수 있다.
- 배너로드의 경우 `RGB8bit`를 사용한다.
- 폰트 높이 통일 설정은 배너로드에 딱히 상관없는 듯
- 주변에 번져야 하고 번진 것들끼리 겹치면 안 되니 패딩은 폰트 사이즈의 1/4 정도를 준다. (필요한 만큼 주면 된다.)

## SDF Texture Generator
Documentation https://catlikecoding.com/sdf-toolkit/docs/texture-generator/

- 폰트 전용 프로그램은 아니고, 이름 그대로 `SDF Texture Generator`이다.
- 설명을 잘 읽어야 하고 본인이 사용하는 폰트 텍스쳐가 어떤 유형인지, 혹은 대상 프로그램이 요구하는 폰트 텍스쳐 인식 방식이 어떤지 잘 알아야 한다.
- 유니티의 에셋스토어에서 임포트하여 사용하였다.

1. 알파 값을 기준으로 `SDF`를 적용하기 때문에 현재 폰트 택스쳐가 어느 채널에 폰트 이미지를 담고 있는지 확인한 후

2. 인스펙터 창에서 그 채널이 알파 채널로 노출되도록 조정하여준다.
본인은 `RGB8bit` 텍스쳐를 사용하였으므로 인스펙터창에서
텍스쳐 타입 - 싱글 채널
채널 - 알파
알파 소스 - 프롬 그레이 스케일
이렇게 설정하여 `RGB8bit` 데이터를 `Alpha8`로 표현되도록 하였다.

3. 그 후 위 링크의 설명에 따라 `Window/SDF Texture Generator`를 열어서 원하는 만큼 세팅하고 `Generate`버튼을 누른다.
폰트 사이즈의 1/4 정도가 적당하다.  (물론 자유)

5. 그러면 우측에 생성된 알파 채널이 보인다.
알파 채널이므로 생성 후 인스팩터창을 통해 채널을 조정하던지, 아니면 처음 세팅에서 `RGB Fill Mode`를 적절히 설정하여 원하는 아웃풋을 얻을 수 있도록 하자.
본인은 `RGB Fill Mode - Distance`로 설정 후 `Generate`하여 RGBA 4개 채널 모두 같은 내용이 되게 하였다.


별의별 툴을 찾아서 써봤지만, 이것만 성공했다. (포맷이 다르다거나 등등 여러 문제)

# 기타
- Distance Field Based Rendering of AngelCode Fonts
http://bitsquid.blogspot.com/2010/04/distance-field-based-rendering-of.html
-BMFont64의 결과 포맷을 기준으로 만든 툴
-.tga 이미지만 지원한다. (배너로드가 읽기는한다.)
-일부러 큰 해상도 텍스쳐를 만든 뒤 반감시키는 방식으로 작동한다.
 -그 와중에 .fnt 파일의 폰트 좌표 및 오프셋 데이터가 함께 반감되므로
 폰트 데이터에 소수가 생긴다. (주의! 배너로드는 오류가 발생)

- msdf-bmfont-xml
https://github.com/soimy/msdf-bmfont-xml
Node.js NPM 으로.