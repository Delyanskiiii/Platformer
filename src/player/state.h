#pragma once
#include "context.h"

class State
{
    protected:
        Context *context_;

    public:
        virtual ~State();
        void set_context(Context *context);
        virtual void Handle1() = 0;
        virtual void Handle2() = 0;
};