
SBOUTPUT=../..

SBCOMMON=SBNotify.vb SBDevices.vb SBHouse.vb SBSecurity.vb SBSingleton.vb Skyview.vb

all: $(SBOUTPUT)/SBSecurityMaster.vb $(SBOUTPUT)/SBHelper.vb

FORCE:

$(SBOUTPUT)/SBSecurityMaster.vb: FORCE
	rm -f $@
	cat SBSecurityMaster.header.vb $(SBCOMMON) SBSecurityMaster.footer.vb | sed 's/\xEF\xBB\xBF//'|dos2unix > $@


$(SBOUTPUT)/SBHelper.vb: FORCE
	rm -f  $@
	cat SBHelper.header.vb $(SBCOMMON) SBHelper.footer.vb |  sed 's/\xEF\xBB\xBF//'| dos2unix > $@
