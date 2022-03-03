import React, {FC} from 'react';
import {Button} from 'antd';

type Props = {
    loading?: boolean | undefined
    isSubmit?: boolean | undefined
};

export const ButtonCreate: FC<Props> = ({loading, isSubmit}) => {
    return (
        <Button loading={loading} type={'primary'} htmlType={!!isSubmit ? 'submit' : 'button'}>Створити</Button>
    );
};
