!include x64.nsh

; WSU Installation script

;--------------------------------
;Include Modern UI

  !include "MUI2.nsh"

;--------------------------------
;Definition of shared names and titles

!define APP_NAME "Wastedge Data Warehouse"
!define APP_SETUP "Wastedge Data Warehouse.exe"
!define APP_TARGET_POSTFIX "${APP_NAME}"
!define APP_EXE "Wastedge.DataWarehouse.Manager.exe"
!define APP_REGBASE "Software\Wastedge Data Warehouse"

;--------------------------------
;Setup

Function .onInit
  ${If} ${RunningX64}
    StrCpy $INSTDIR "$PROGRAMFILES64\${APP_TARGET_POSTFIX}"
  ${Else}
    StrCpy $INSTDIR "$PROGRAMFILES\${APP_TARGET_POSTFIX}"
  ${EndIf}
  
  ReadRegStr $1 HKLM "${APP_REGBASE}" "Installation path"
  StrCmp $1 "" installation_path_is_present
  
  StrCpy $INSTDIR $1
  
installation_path_is_present:
FunctionEnd

;--------------------------------
;General

  ;Name and file
  Name "${APP_NAME}"
  OutFile "${APP_SETUP}"
  
  ;Default installation folder
  InstallDir "$PROGRAMFILES\${APP_TARGET_POSTFIX}"
  
  ;Request application privileges for Windows Vista
  RequestExecutionLevel admin

;--------------------------------
;Variables

  Var StartMenuFolder

;--------------------------------
;Interface Settings

  !define MUI_ABORTWARNING
  
  !define MUI_ICON "icon.ico"
  
  !define MUI_FINISHPAGE_RUN "$INSTDIR\${APP_EXE}"
  !define MUI_FINISHPAGE_RUN_TEXT "Run ${APP_NAME}"

;--------------------------------
;Pages

  !define MUI_PAGE_CUSTOMFUNCTION_SHOW DisableDirectoryField
  !insertmacro MUI_PAGE_DIRECTORY

  ;Start Menu Folder Page Configuration
  !define MUI_STARTMENUPAGE_REGISTRY_ROOT "HKLM"
  !define MUI_STARTMENUPAGE_REGISTRY_KEY "${APP_REGBASE}" 
  !define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "Start Menu Folder"

  !insertmacro MUI_PAGE_STARTMENU Application $StartMenuFolder

  !insertmacro MUI_PAGE_INSTFILES
  !insertmacro MUI_PAGE_FINISH
  
;--------------------------------
;Languages
 
  !insertmacro MUI_LANGUAGE "English"

;--------------------------------
;Installer Sections

Section ""
    
  ; Set the correct installation folder
  SetOutPath "$INSTDIR"
  
  ; Write the Run entry
  WriteRegStr HKLM "${APP_REGBASE}" "Installation path" "$INSTDIR"

  ; Install all files
  File /r /x *.zip /x *.xml /x *.vshost.* /x *.pdb /x *.manifest /x *.config "..\bin\Release\*.*"
  
  ;Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"
  
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
  
    ;Create shortcuts
    CreateDirectory "$SMPROGRAMS\$StartMenuFolder"
    CreateShortCut "$SMPROGRAMS\$StartMenuFolder\Wastedge Data Warehouse Manager.lnk" "$INSTDIR\${APP_EXE}"
    CreateShortCut "$SMPROGRAMS\$StartMenuFolder\Uninstall.lnk" "$INSTDIR\Uninstall.exe"
  
  !insertmacro MUI_STARTMENU_WRITE_END

SectionEnd

;--------------------------------
;Uninstaller Section

Section "Uninstall"
  
  RMDir /r "$INSTDIR"
  
  !insertmacro MUI_STARTMENU_GETFOLDER Application $StartMenuFolder
    
  Delete "$SMPROGRAMS\$StartMenuFolder\Uninstall.lnk"
  Delete "$SMPROGRAMS\$StartMenuFolder\Wastedge Data Warehouse Manager.lnk" 
  RMDir "$SMPROGRAMS\$StartMenuFolder"
  
  DeleteRegKey HKLM "${APP_REGBASE}"

SectionEnd

Function DisableDirectoryField
  ReadRegStr $1 HKLM "${APP_REGBASE}" "Installation path"
  StrCmp $1 "" installation_path_is_present

  FindWindow $R0 "#32770" "" $HWNDPARENT
  GetDlgItem $R1 $R0 1019 ;Text Box
  EnableWindow $R1 0
  
installation_path_is_present:

FunctionEnd
